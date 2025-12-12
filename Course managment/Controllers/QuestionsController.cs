using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions([FromQuery] int? quizId)
        {
            List<Question> questions;
            if (quizId.HasValue)
            {
                questions = await _questionService.GetQuestionsByQuizAsync(quizId.Value);
            }
            else
            {
                questions = new List<Question>();
            }
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(QuestionDTO question)
        {
            var createdQuestion = await _questionService.CreateQuestionAsync(question);
            return CreatedAtAction(nameof(GetQuestion), new { id = createdQuestion.QuestionId }, createdQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionDTO question)
        {
            var existingQuestion = await _questionService.GetQuestionByIdAsync(id);
            if (id != existingQuestion.QuestionId) return BadRequest();
            await _questionService.UpdateQuestionAsync(question);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}