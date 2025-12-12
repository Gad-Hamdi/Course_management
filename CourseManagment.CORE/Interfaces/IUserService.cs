using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Identity;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUserAsync(CreateUserRequest user, string password);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<ApplicationUser>> GetUsersByCompanyAsync(int companyId);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<UserStatistics> GetUserStatisticsAsync(string userId);
        Task<int> GetUserTotalPointsAsync(string userId);
        Task<int> GetUserCompletedCoursesAsync(string userId);
        Task<int> GetUserBadgesCountAsync(string userId);

        Task<List<ApplicationUser>> BulkCreateUsersAsync(List<ApplicationUser> users, string defaultPassword);
        Task<string> GenerateBulkImportTemplateAsync();
    }

    public class UserStatistics
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int TotalPoints { get; set; }
        public int CompletedCourses { get; set; }
        public int ActiveEnrollments { get; set; }
        public int BadgesCount { get; set; }
        public int QuizAttempts { get; set; }
        public int PassedQuizzes { get; set; }
        public decimal AverageQuizScore { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}