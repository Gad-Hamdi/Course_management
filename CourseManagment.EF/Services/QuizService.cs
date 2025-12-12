using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Quiz> CreateQuizAsync(QuizDTO quizDTO)
        {
            var quiz = quizDTO.Adapt<Quiz>();
            await _unitOfWork.Quizzes.CreateAsync(quiz);
            await _unitOfWork.CompleteAsync();
            return quiz;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            return await _unitOfWork.Quizzes.GetOneAsync(q => q.QuizId == id);
        }

        public async Task<List<Quiz>> GetQuizzesByCourseAsync(int courseId)
        {
            return await _unitOfWork.Quizzes.GetAsync(q => q.CourseId == courseId);
        }

        public async Task UpdateQuizAsync(QuizDTO quizDTO)
        {
            var quiz = quizDTO.Adapt<Quiz>();
            _unitOfWork.Quizzes.Update(quiz);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await GetQuizByIdAsync(id);
            if (quiz != null)
            {
                _unitOfWork.Quizzes.Delete(quiz);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}