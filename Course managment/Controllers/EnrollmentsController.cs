using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using CourseManagment.EF.Services;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateEnrollment(Enrollment enrollment)
        {
            var createdEnrollment = await _enrollmentService.EnrollUserAsync(
                enrollment.UserId,
                enrollment.CourseId
            );
            return CreatedAtAction(nameof(GetEnrollment), new { id = createdEnrollment.EnrollmentId }, createdEnrollment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentAsync(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Enrollment>>> GetUserEnrollments(string userId)
        {
            var enrollments = await _enrollmentService.GetUserEnrollmentsAsync(userId);
            return Ok(enrollments);
        }

        [HttpPut("{id}/progress")]
        public async Task<IActionResult> UpdateProgress(int id, [FromBody] decimal progress)
        {
            await _enrollmentService.UpdateProgressAsync(id, progress);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteEnrollmentAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id,  EnrollmentDTO enrollment)
        {
            var existingEnrollment = await _enrollmentService.GetEnrollmentAsync(id);
            if (existingEnrollment == null || id != existingEnrollment.EnrollmentId) return BadRequest();
            await _enrollmentService.UpdateEnrollmentAsync(enrollment);
            return NoContent();
        }
    }
}