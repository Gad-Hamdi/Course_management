using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Question> CreateQuestionAsync(QuestionDTO questionDTO)
        {
            var question = questionDTO.Adapt<Question>();
            await _unitOfWork.Questions.CreateAsync(question);
            await _unitOfWork.CompleteAsync();
            return question;
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _unitOfWork.Questions.GetOneAsync(q => q.QuestionId == id);
        }

        public async Task<List<Question>> GetQuestionsByQuizAsync(int quizId)
        {
            return await _unitOfWork.Questions.GetAsync(q => q.QuizId == quizId);
        }

        public async Task UpdateQuestionAsync(QuestionDTO questionDTO)
        {
            var question = questionDTO.Adapt<Question>();
            _unitOfWork.Questions.Update(question);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await GetQuestionByIdAsync(id);
            if (question != null)
            {
                _unitOfWork.Questions.Delete(question);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}