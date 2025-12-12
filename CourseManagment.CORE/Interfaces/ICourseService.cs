using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface ICourseService
    {
        Task<Course> CreateCourseAsync(courseRequest course);
        Task<Course?> GetCourseByIdAsync(int id);
        Task<List<Course>> GetCoursesByCompanyAsync(int companyId);
        Task UpdateCourseAsync(int id,courseRequest courseRequest);
        Task DeleteCourseAsync(int id);
    }
}