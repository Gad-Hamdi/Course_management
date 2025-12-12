using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;

namespace CourseManagment.EF.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public QuizAttemptService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<QuizAttempt> StartQuizAttemptAsync(string userId, int quizId)
        {
            // Check if user exists
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var attempt = new QuizAttempt
            {
                UserId = userId,
                QuizId = quizId,
                AttemptDate = DateTime.UtcNow,
                Passed = false
            };

            await _unitOfWork.QuizAttempts.CreateAsync(attempt);
            await _unitOfWork.CompleteAsync();
            return attempt;
        }

        public async Task<QuizAttempt?> GetQuizAttemptByIdAsync(int attemptId)
        {
            return await _unitOfWork.QuizAttempts.GetOneAsync(q => q.QuizAttemptId == attemptId);
        }

        public async Task<List<QuizAttempt>> GetUserQuizAttemptsAsync(string userId)
        {
            return await _unitOfWork.QuizAttempts.GetAsync(q => q.UserId == userId);
        }

        public async Task<QuizAttempt> SubmitQuizAttemptAsync(int attemptId, Dictionary<int, string> answers)
        {
            var attempt = await GetQuizAttemptByIdAsync(attemptId);
            if (attempt == null)
                throw new ArgumentException("Quiz attempt not found");

            // Simple grading logic
            var questions = await _unitOfWork.Questions.GetAsync(q => q.QuizId == attempt.QuizId);
            var quiz = await _unitOfWork.Quizzes.GetOneAsync(q => q.QuizId == attempt.QuizId);

            int correctAnswers = 0;
            foreach (var question in questions)
            {
                if (answers.ContainsKey(question.QuestionId) &&
                    answers[question.QuestionId] == question.CorrectAnswer)
                {
                    correctAnswers++;
                }
            }

            attempt.Score = (int)((correctAnswers / (double)questions.Count) * 100);
            attempt.Passed = quiz?.PassingScore == null || attempt.Score >= quiz.PassingScore;

            _unitOfWork.QuizAttempts.Update(attempt);
            await _unitOfWork.CompleteAsync();

            return attempt;
        }
    }
}