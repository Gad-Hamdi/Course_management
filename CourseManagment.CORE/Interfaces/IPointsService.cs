using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IPointsService
    {
        Task AwardPointsAsync(string userId, int points, string reason);
        Task<List<PointTransaction>> GetUserPointsAsync(string userId);
        Task<int> GetUserTotalPointsAsync(string userId);
    }
}