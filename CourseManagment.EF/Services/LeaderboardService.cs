using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;

namespace CourseManagment.EF.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public LeaderboardService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<List<LeaderboardEntry>> GetCompanyLeaderboardAsync(int companyId, int top = 10)
        {
            // Get users by company using UserService
            var users = await _userService.GetUsersByCompanyAsync(companyId);

            var leaderboard = new List<LeaderboardEntry>();
            int rank = 1;

            foreach (var user in users.OrderByDescending(u => u.Points).Take(top))
            {
                var badges = await _unitOfWork.EmployeeBadges.GetAsync(eb => eb.UserId == user.Id);
                var completedCourses = await _unitOfWork.Enrollments.GetAsync(
                    e => e.UserId == user.Id && e.Status == "Completed"
                );

                leaderboard.Add(new LeaderboardEntry
                {
                    UserId = user.Id,
                    UserName = user.FullName,
                    Points = user.Points,
                    Rank = rank++,
                    BadgesCount = badges.Count,
                    CompletedCourses = completedCourses.Count
                });
            }

            return leaderboard;
        }

        public async Task<List<LeaderboardEntry>> GetCourseLeaderboardAsync(int courseId, int top = 10)
        {
            var enrollments = await _unitOfWork.Enrollments.GetAsync(
                e => e.CourseId == courseId
            );

            var leaderboard = new List<LeaderboardEntry>();
            int rank = 1;

            foreach (var enrollment in enrollments.OrderByDescending(e => e.Progress).Take(top))
            {
                var user = await _userService.GetUserByIdAsync(enrollment.UserId);
                if (user != null)
                {
                    var badges = await _unitOfWork.EmployeeBadges.GetAsync(eb => eb.UserId == user.Id);

                    leaderboard.Add(new LeaderboardEntry
                    {
                        UserId = user.Id,
                        UserName = user.FullName,
                        Points = user.Points,
                        Rank = rank++,
                        BadgesCount = badges.Count,
                        CompletedCourses = 0
                    });
                }
            }

            return leaderboard;
        }

        public async Task<LeaderboardEntry> GetUserRankAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            // Get all users in the same company using UserService
            var companyUsers = await _userService.GetUsersByCompanyAsync(user.CompanyId);

            var sortedUsers = companyUsers.OrderByDescending(u => u.Points).ToList();
            var rank = sortedUsers.FindIndex(u => u.Id == userId) + 1;

            var badges = await _unitOfWork.EmployeeBadges.GetAsync(eb => eb.UserId == userId);
            var completedCourses = await _unitOfWork.Enrollments.GetAsync(
                e => e.UserId == userId && e.Status == "Completed"
            );

            return new LeaderboardEntry
            {
                UserId = user.Id,
                UserName = user.FullName,
                Points = user.Points,
                Rank = rank,
                BadgesCount = badges.Count,
                CompletedCourses = completedCourses.Count
            };
        }
    }
}