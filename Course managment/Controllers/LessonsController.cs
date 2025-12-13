using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }
        [HttpGet]
        public async Task<IActionResult> GetLessonsByCourseIdAsync([FromQuery] int courseId)
        {
            var lessons = await _lessonService.GetLessonsByCourseIdAsync(courseId);
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(LessonRequest lessonRequest)
        {
            var createdLesson = await _lessonService.CreateLessonAsync(lessonRequest);
            return CreatedAtAction(nameof(GetLesson), new { id = createdLesson.LessonId }, createdLesson);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id,LessonRequest lessonRequest)
        {
            var existinglesson = await _lessonService.GetLessonByIdAsync(id);
            if (existinglesson == null || id != existinglesson.LessonId) return BadRequest();
            await _lessonService.UpdateLessonAsync( lessonRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return NoContent();
        }

    }
}
    
    
