using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Interfaces
{
    public interface ILessonService
    {
        Task<Lesson> CreateLessonAsync(LessonRequest lessonRequest);
        Task<Lesson?> GetLessonByIdAsync(int id);
        Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId);
        Task UpdateLessonAsync(LessonRequest lessonRequest);
        Task DeleteLessonAsync(int id);

    }
}
