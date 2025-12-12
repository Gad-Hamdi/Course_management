using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        public BadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Badge>>> GetBadges()
        {
            var badges = await _badgeService.GetAllBadgesAsync();
            return Ok(badges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(int id)
        {
            var badge = await _badgeService.GetBadgeByIdAsync(id);
            if (badge == null) return NotFound();
            return Ok(badge);
        }

        [HttpPost]
        public async Task<ActionResult<Badge>> CreateBadge(BadgeDTO badge)
        {
            var createdBadge = await _badgeService.CreateBadgeAsync(badge);
            return CreatedAtAction(nameof(GetBadge), new { id = createdBadge.BadgeId }, createdBadge);
        }

        [HttpPost("award")]
        public async Task<IActionResult> AwardBadge(AwardBadgeRequest request)
        {
            await _badgeService.AwardBadgeToUserAsync(request.UserId, request.BadgeId);
            return Ok(new { message = "Badge awarded successfully" });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Badge>>> GetUserBadges(string userId)
        {
            var badges = await _badgeService.GetUserBadgesAsync(userId);
            return Ok(badges);
        }
        
        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeleteBadge(int id)
        {
            await _badgeService.DeleteBadgeAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBadge(int id, BadgeDTO badge)
        {
            var existingBadge = await _badgeService.GetBadgeByIdAsync(id);
            if (existingBadge == null || id != existingBadge.BadgeId) return BadRequest();
            await _badgeService.UpdateBadgeAsync(badge);
            return NoContent();
        }
    }

    
}