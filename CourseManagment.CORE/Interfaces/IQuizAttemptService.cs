using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IQuizAttemptService
    {
        Task<QuizAttempt> StartQuizAttemptAsync(string userId, int quizId);
        Task<QuizAttempt?> GetQuizAttemptByIdAsync(int attemptId);
        Task<List<QuizAttempt>> GetUserQuizAttemptsAsync(string userId);
        Task<QuizAttempt> SubmitQuizAttemptAsync(int attemptId, Dictionary<int, string> answers);
    }
}