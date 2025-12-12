using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface ILeaderboardService
    {
        Task<List<LeaderboardEntry>> GetCompanyLeaderboardAsync(int companyId, int top = 10);
        Task<List<LeaderboardEntry>> GetCourseLeaderboardAsync(int courseId, int top = 10);
        Task<LeaderboardEntry> GetUserRankAsync(string userId);
    }
}