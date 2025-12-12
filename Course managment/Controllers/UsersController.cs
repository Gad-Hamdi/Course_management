using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.DTOs.Response;
using CourseManagment.CORE.Identity;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUser>>> GetUsers([FromQuery] int? companyId)
        {
            List<ApplicationUser> users;
            if (companyId.HasValue)
            {

                users = await _userService.GetUsersByCompanyAsync(companyId.Value);
            }
            else
            {
                users = await _userService.GetAllUsersAsync();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<ApplicationUser>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> CreateUser(CreateUserRequest request)
        {
            

            var createdUser = await _userService.CreateUserAsync(request, request.Password);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, ApplicationUser user)
        {
            if (id != user.Id) return BadRequest();
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("bulk-import")]
        public async Task<ActionResult<BulkImportResponse>> BulkImportUsers(BulkImportRequest request)
        {
            var createdUsers = await _userService.BulkCreateUsersAsync(request.Users, request.DefaultPassword);
            return Ok(new BulkImportResponse
            {
                TotalProcessed = request.Users.Count,
                SuccessCount = createdUsers.Count,
                FailedCount = request.Users.Count - createdUsers.Count,
                CreatedUsers = createdUsers
            });
        }

        [HttpGet("bulk-import/template")]
        public async Task<ActionResult> GetBulkImportTemplate()
        {
            var template = await _userService.GenerateBulkImportTemplateAsync();
            return File(Encoding.UTF8.GetBytes(template), "text/csv", "user_import_template.csv");
        }

        [HttpGet("{id}/statistics")]
        public async Task<ActionResult<UserStatistics>> GetUserStatistics(string id)
        {
            var statistics = await _userService.GetUserStatisticsAsync(id);
            return Ok(statistics);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdatePassword(string id, UpdatePasswordRequest request)
        {
            await _userService.UpdatePasswordAsync(id, request.CurrentPassword, request.NewPassword);
            return Ok(new { message = "Password updated successfully" });
        }

        [HttpPost("{id}/check-password")]
        public async Task<ActionResult<bool>> CheckPassword(string id, CheckPasswordRequest request)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var isValid = await _userService.CheckPasswordAsync(user, request.Password);
            return Ok(isValid);
        }
    }

   
}