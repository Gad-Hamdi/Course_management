using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourseManagment.EF.Services
{
    public class LessonService: ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Lesson> CreateLessonAsync(LessonRequest lessonRequest)
        {
            var lesson = lessonRequest.Adapt<Lesson>();
            await _unitOfWork.Lessons.CreateAsync(lesson);
            await _unitOfWork.CompleteAsync();
            return lesson;
        }

        public async Task DeleteLessonAsync(int id)
        {
            var lesson = await GetLessonByIdAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            }
            
                _unitOfWork.Lessons.Delete(lesson);
                await _unitOfWork.CompleteAsync();
              }

        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(int cousreId)
        {
            return await _unitOfWork.Lessons.GetAsync(c => c.CourseId == cousreId);
        }


        public async Task<Lesson?> GetLessonByIdAsync(int id)
        {
            return await _unitOfWork.Lessons.GetOneAsync(c => c.LessonId == id);
        }

        public async Task UpdateLessonAsync( LessonRequest lessonRequest)
        {
            var lesson =lessonRequest.Adapt<Lesson>();
            _unitOfWork.Lessons.Update(lesson);
            await _unitOfWork.CompleteAsync();

        }
    }
}
