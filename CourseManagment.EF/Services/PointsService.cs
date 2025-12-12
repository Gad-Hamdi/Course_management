using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;

namespace CourseManagment.EF.Services
{
    public class PointsService : IPointsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PointsService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task AwardPointsAsync(string userId, int points, string reason)
        {
            var transaction = new PointTransaction
            {
                UserId = userId,
                Points = points,
                Reason = reason,
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.PointTransactions.CreateAsync(transaction);

            // Update user's total points using UserService
            var user = await _userService.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Points += points;
                await _userService.UpdateUserAsync(user);
            }
            else
            {
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<PointTransaction>> GetUserPointsAsync(string userId)
        {
            return await _unitOfWork.PointTransactions.GetAsync(
                p => p.UserId == userId,
                orderBy: q => q.OrderByDescending(p => p.TransactionDate)
            );
        }

        public async Task<int> GetUserTotalPointsAsync(string userId)
        {
            return await _userService.GetUserTotalPointsAsync(userId);
        }
    }
}