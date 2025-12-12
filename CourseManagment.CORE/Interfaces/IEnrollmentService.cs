using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Models;

namespace CourseManagment.CORE.Interfaces
{
    public interface IEnrollmentService
    {
        Task<Enrollment> EnrollUserAsync(string userId, int courseId);
        Task<Enrollment?> GetEnrollmentAsync(int enrollmentId);
        Task<List<Enrollment>> GetUserEnrollmentsAsync(string userId);
        Task UpdateProgressAsync(int enrollmentId, decimal progress);

        Task DeleteEnrollmentAsync(int id);
        Task UpdateEnrollmentAsync(EnrollmentDTO enrollment);
        Task<List<Enrollment>> GetEnrollmentsByCourseAsync(int courseId);
        Task CompleteEnrollmentAsync(int enrollmentId);
    }
}