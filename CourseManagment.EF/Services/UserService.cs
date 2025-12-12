using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Identity;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CourseManagment.EF.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CreateUserAsync(CreateUserRequest userRequest, string password)
        {
            var user = userRequest.Adapt<ApplicationUser>();
            // Check if company exists
            var company = await _unitOfWork.Companies.GetOneAsync(c => c.CompanyId == user.CompanyId);
            if (company == null)
                throw new ArgumentException("Company does not exist");

            // Set additional properties
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;
            user.EmailConfirmed = true;

            // Create user using Identity
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }

            // Assign default role (Employee)
            await _userManager.AddToRoleAsync(user, "Employee");

            return user;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersByCompanyAsync(int companyId)
        {
            return await _userManager.Users
                .Where(u => u.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update user: {errors}");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                // Soft delete by deactivating
                user.IsActive = false;
                await UpdateUserAsync(user);
            }
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to change password: {errors}");
            }
        }

        public async Task<UserStatistics> GetUserStatisticsAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var enrollments = await _unitOfWork.Enrollments.GetAsync(e => e.UserId == userId);
            var quizAttempts = await _unitOfWork.QuizAttempts.GetAsync(q => q.UserId == userId);
            var badges = await _unitOfWork.EmployeeBadges.GetAsync(eb => eb.UserId == userId);

            var completedCourses = enrollments.Count(e => e.Status == "Completed");
            var activeEnrollments = enrollments.Count(e => e.Status == "Active");
            var passedQuizzes = quizAttempts.Count(q => q.Passed);
            var averageScore = quizAttempts.Any() ? quizAttempts.Average(q => q.Score ?? 0) : 0;

            var lastActivity = GetLastActivity(userId, enrollments, quizAttempts);

            return new UserStatistics
            {
                UserId = user.Id,
                UserName = user.FullName,
                TotalPoints = user.Points,
                CompletedCourses = completedCourses,
                ActiveEnrollments = activeEnrollments,
                BadgesCount = badges.Count,
                QuizAttempts = quizAttempts.Count,
                PassedQuizzes = passedQuizzes,
                AverageQuizScore = (decimal)Math.Round(averageScore, 2),
                LastActivity = lastActivity
            };
        }

        public async Task<int> GetUserTotalPointsAsync(  string userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user?.Points ?? 0;
        }

        public async Task<int> GetUserCompletedCoursesAsync(string userId)
        {
            var enrollments = await _unitOfWork.Enrollments.GetAsync(e => e.UserId == userId);
            return enrollments.Count(e => e.Status == "Completed");
        }

        public async Task<int> GetUserBadgesCountAsync(string userId)
        {
            var badges = await _unitOfWork.EmployeeBadges.GetAsync(eb => eb.UserId == userId);
            return badges.Count;
        }

        public async Task<List<ApplicationUser>> BulkCreateUsersAsync(List<ApplicationUser> users, string defaultPassword)
        {
            var createdUsers = new List<ApplicationUser>();
            var errors = new List<string>();

            foreach (var user in users)
            {
                try
                {
                    var request = user.Adapt<CreateUserRequest>();
                    var createdUser = await CreateUserAsync(request, defaultPassword);
                    createdUsers.Add(createdUser);
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to create user {user.Email}: {ex.Message}");
                }
            }

            if (errors.Any())
            {
                throw new InvalidOperationException($"Bulk creation completed with errors: {string.Join("; ", errors)}");
            }

            return createdUsers;
        }

        public async Task<string> GenerateBulkImportTemplateAsync()
        {
            var template = new StringBuilder();
            template.AppendLine("FirstName,LastName,Email,JobTitle,CompanyId");
            template.AppendLine("John,Doe,john.doe@company.com,Software Engineer,1");
            template.AppendLine("Jane,Smith,jane.smith@company.com,Product Manager,1");

            return template.ToString();
        }

        private DateTime? GetLastActivity(string userId, List<Enrollment> enrollments, List<QuizAttempt> quizAttempts)
        {
            var lastEnrollment = enrollments.MaxBy(e => e.EnrolledAt)?.EnrolledAt;
            var lastQuizAttempt = quizAttempts.MaxBy(q => q.AttemptDate)?.AttemptDate;

            if (lastEnrollment.HasValue && lastQuizAttempt.HasValue)
                return lastEnrollment.Value > lastQuizAttempt.Value ? lastEnrollment.Value : lastQuizAttempt.Value;

            return lastEnrollment ?? lastQuizAttempt;
        }
    }
}