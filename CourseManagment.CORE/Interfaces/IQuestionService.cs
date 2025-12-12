using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(QuestionDTO question);
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<List<Question>> GetQuestionsByQuizAsync(int quizId);
        Task UpdateQuestionAsync(QuestionDTO question);
        Task DeleteQuestionAsync(int id);
    }
}