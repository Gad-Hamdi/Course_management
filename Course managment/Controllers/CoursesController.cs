using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses([FromQuery] int? companyId)
        {
            List<Course> courses;
            if (companyId.HasValue)
            {
                courses = await _courseService.GetCoursesByCompanyAsync(companyId.Value);
            }
            else
            {
                courses = new List<Course>();
            }
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(courseRequest course)
        {
            var createdCourse = await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseId }, createdCourse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, courseRequest courseRequest)
        {
            await _courseService.UpdateCourseAsync(id, courseRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}