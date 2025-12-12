using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> CreateCourseAsync(courseRequest courseRequest)
        {
            var course = courseRequest.Adapt<Course>(); 
            course.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Courses.CreateAsync(course);
            await _unitOfWork.CompleteAsync();
            return course;
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _unitOfWork.Courses.GetOneAsync(c => c.CourseId == id);
        }

        public async Task<List<Course>> GetCoursesByCompanyAsync(int companyId)
        {
            return await _unitOfWork.Courses.GetAsync(c => c.CompanyId == companyId);
        }

        public async Task UpdateCourseAsync(int id,courseRequest courseRequest)
        {
            var course = await GetCourseByIdAsync(id);
            if (course is null)
            {
                throw new KeyNotFoundException($"Course with ID {id} not found.");
            }
            course.Title = courseRequest.Title;
            course.Description = courseRequest.Description;
            course.Category = courseRequest.Category;
            course.Duration = courseRequest.Duration;
            course.CompanyId = courseRequest.CompanyId;
            

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            if (course != null)
            {
                _unitOfWork.Courses.Delete(course);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}