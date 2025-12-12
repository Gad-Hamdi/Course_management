using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public BadgeService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Badge> CreateBadgeAsync(BadgeDTO badgeDTO)
        {
            var badge = badgeDTO.Adapt<Badge>();
            await _unitOfWork.Badges.CreateAsync(badge);
            await _unitOfWork.CompleteAsync();
            return  badge ;
        }

        public async Task<Badge?> GetBadgeByIdAsync(int id)
        {
            var Badge = await _unitOfWork.Badges.GetOneAsync(b => b.BadgeId == id);
            if (Badge == null)
                throw new ArgumentException("Badge not found");

            return await _unitOfWork.Badges.GetOneAsync(b => b.BadgeId == id);
        }

        public async Task<List<Badge>> GetAllBadgesAsync()
        {

            return await _unitOfWork.Badges.GetAsync();
        }

        public async Task AwardBadgeToUserAsync(string userId, int badgeId)
        {
            // Check if user exists using UserService
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var Badge = await _unitOfWork.Badges.GetOneAsync(b=>b.BadgeId==badgeId);
            if (Badge == null)
                throw new ArgumentException("Badge not found");

            // Check if user already has this badge
            var existingBadge = await _unitOfWork.EmployeeBadges.GetOneAsync(
                eb => eb.UserId == userId && eb.BadgeId == badgeId
            );

            if (existingBadge == null)
            {
                var employeeBadge = new EmployeeBadge
                {
                    UserId = userId,
                    BadgeId = badgeId,
                    AwardedAt = DateTime.UtcNow
                };

                await _unitOfWork.EmployeeBadges.CreateAsync(employeeBadge);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<Badge>> GetUserBadgesAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var userBadges = await _unitOfWork.EmployeeBadges.GetAsync(
                eb => eb.UserId == userId
            );
            if (userBadges == null)
                throw new ArgumentException("userBadges not found");

            var badgeIds = userBadges.Select(ub => ub.BadgeId).ToList();
            return await _unitOfWork.Badges.GetAsync(b => badgeIds.Contains(b.BadgeId));
        }
        
        public async Task UpdateBadgeAsync(BadgeDTO badgeDTO)
        {
            var badge = badgeDTO.Adapt<Badge>();
            _unitOfWork.Badges.Update(badge);
            await _unitOfWork.CompleteAsync();

        }

        public async Task DeleteBadgeAsync(int id)
        {
            var badge = await GetBadgeByIdAsync(id);
            if (badge != null)
            {
                _unitOfWork.Badges.Delete(badge);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}