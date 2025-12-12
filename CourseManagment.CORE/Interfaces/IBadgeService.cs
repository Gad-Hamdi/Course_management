using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IBadgeService
    {
        Task<Badge> CreateBadgeAsync(BadgeDTO badgeDTO);
        Task<Badge?> GetBadgeByIdAsync(int id);
        Task<List<Badge>> GetAllBadgesAsync();
        Task AwardBadgeToUserAsync(string userId, int badgeId);
        Task<List<Badge>> GetUserBadgesAsync(string userId);
        Task UpdateBadgeAsync(BadgeDTO badgeDTO );
        Task DeleteBadgeAsync(int id);
    }
}
