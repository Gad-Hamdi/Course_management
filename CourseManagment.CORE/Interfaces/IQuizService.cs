using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuizAsync(QuizDTO quiz);
        Task<Quiz?> GetQuizByIdAsync(int id);
        Task<List<Quiz>> GetQuizzesByCourseAsync(int courseId);
        Task UpdateQuizAsync(QuizDTO quiz);
        Task DeleteQuizAsync(int id);
    }
}