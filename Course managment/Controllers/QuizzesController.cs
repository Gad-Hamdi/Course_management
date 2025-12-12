using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Quiz>>> GetQuizzes([FromQuery] int? courseId)
        {
            List<Quiz> quizzes;
            if (courseId.HasValue)
            {
                quizzes = await _quizService.GetQuizzesByCourseAsync(courseId.Value);
            }
            else
            {
                quizzes = new List<Quiz>();
            }
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null) return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> CreateQuiz(QuizDTO quiz)
        {
            var createdQuiz = await _quizService.CreateQuizAsync(quiz);
            return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.QuizId }, createdQuiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, QuizDTO quiz)
        {
            var existingQuiz = await _quizService.GetQuizByIdAsync(id);
            if (id != existingQuiz.QuizId) return BadRequest();
            await _quizService.UpdateQuizAsync(quiz);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            await _quizService.DeleteQuizAsync(id);
            return NoContent();
        }
    }
}