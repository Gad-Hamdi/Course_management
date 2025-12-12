using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.DTOs.Response;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        [HttpPost("award")]
        public async Task<IActionResult> AwardPoints(AwardPointsRequest request)
        {
            await _pointsService.AwardPointsAsync(request.UserId, request.Points, request.Reason);
            return Ok(new { message = "Points awarded successfully" });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserPointsResponse>> GetUserPoints(string userId)
        {
            var totalPoints = await _pointsService.GetUserTotalPointsAsync(userId);
            var transactions = await _pointsService.GetUserPointsAsync(userId);

            return Ok(new UserPointsResponse
            {
                TotalPoints = totalPoints,
                Transactions = transactions
            });
        }

        [HttpGet("user/{userId}/transactions")]
        public async Task<ActionResult<List<PointTransaction>>> GetPointTransactions(string userId)
        {
            var transactions = await _pointsService.GetUserPointsAsync(userId);
            return Ok(transactions);
        }
    }

}