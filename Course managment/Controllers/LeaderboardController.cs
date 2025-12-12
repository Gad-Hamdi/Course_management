using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<List<LeaderboardEntry>>> GetCompanyLeaderboard(int companyId, [FromQuery] int top = 10)
        {
            var leaderboard = await _leaderboardService.GetCompanyLeaderboardAsync(companyId, top);
            return Ok(leaderboard);
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<List<LeaderboardEntry>>> GetCourseLeaderboard(int courseId, [FromQuery] int top = 10)
        {
            var leaderboard = await _leaderboardService.GetCourseLeaderboardAsync(courseId, top);
            return Ok(leaderboard);
        }

        [HttpGet("user/{userId}/rank")]
        public async Task<ActionResult<LeaderboardEntry>> GetUserRank(string userId)
        {
            var rank = await _leaderboardService.GetUserRankAsync(userId);
            return Ok(rank);
        }
    }
}