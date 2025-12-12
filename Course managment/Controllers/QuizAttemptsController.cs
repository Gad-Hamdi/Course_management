using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizAttemptsController(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<QuizAttempt>> StartQuizAttempt(StartQuizRequest request)
        {
            var attempt = await _quizAttemptService.StartQuizAttemptAsync(request.UserId, request.QuizId);
            return Ok(attempt);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttempt(int id)
        {
            var attempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
            if (attempt == null) return NotFound();
            return Ok(attempt);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetUserQuizAttempts(string userId)
        {
            var attempts = await _quizAttemptService.GetUserQuizAttemptsAsync(userId);
            return Ok(attempts);
        }

        [HttpPost("{id}/submit")]
        public async Task<ActionResult<QuizAttempt>> SubmitQuizAttempt(int id, SubmitQuizRequest request)
        {
            var attempt = await _quizAttemptService.SubmitQuizAttemptAsync(id, request.Answers);
            return Ok(attempt);
        }
    }

    
}