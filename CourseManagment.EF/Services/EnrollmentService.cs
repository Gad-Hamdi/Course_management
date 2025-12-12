using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Mapster;

namespace CourseManagment.EF.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IPointsService _pointsService;

        public EnrollmentService(IUnitOfWork unitOfWork, IUserService userService, IPointsService pointsService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _pointsService = pointsService;
        }

        public async Task<Enrollment> EnrollUserAsync(string userId, int courseId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            // Check if already enrolled
            var existingEnrollment = await _unitOfWork.Enrollments.GetOneAsync(
                e => e.UserId == userId && e.CourseId == courseId
            );

            if (existingEnrollment != null)
                throw new InvalidOperationException("User is already enrolled in this course");

            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                Progress = 0,
                Status = "Active",
                EnrolledAt = DateTime.UtcNow
            };

            await _unitOfWork.Enrollments.CreateAsync(enrollment);
            await _unitOfWork.CompleteAsync();

            // Award points for enrollment
            await _pointsService.AwardPointsAsync(userId, 10, "Course enrollment");

            return enrollment;
        }

        public async Task<Enrollment?> GetEnrollmentAsync(int enrollmentId)
        {
            return await _unitOfWork.Enrollments.GetOneAsync(e => e.EnrollmentId == enrollmentId);
        }

        public async Task<List<Enrollment>> GetUserEnrollmentsAsync(string userId)
        {
            return await _unitOfWork.Enrollments.GetAsync(
                e => e.UserId == userId,
                orderBy: q => q.OrderByDescending(e => e.EnrolledAt)
            );
        }

        public async Task<List<Enrollment>> GetEnrollmentsByCourseAsync(int courseId)
        {
            return await _unitOfWork.Enrollments.GetAsync(
                e => e.CourseId == courseId,
                orderBy: q => q.OrderByDescending(e => e.EnrolledAt)
            );
        }

        public async Task UpdateProgressAsync(int enrollmentId, decimal progress)
        {
            var enrollment = await GetEnrollmentAsync(enrollmentId);
            if (enrollment != null)
            {
                enrollment.Progress = Math.Min(progress, 100);

                if (progress >= 100)
                {
                    enrollment.Status = "Completed";
                    await _pointsService.AwardPointsAsync(enrollment.UserId, 100, "Course completion");
                }

                _unitOfWork.Enrollments.Update(enrollment);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task CompleteEnrollmentAsync(int enrollmentId)
        {
            await UpdateProgressAsync(enrollmentId, 100);
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            var enrollment = await GetEnrollmentAsync(id);
            if (enrollment != null)
            {
                _unitOfWork.Enrollments.Delete(enrollment);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task UpdateEnrollmentAsync(EnrollmentDTO enrollmentDTO)
        {
            var enrollment = await GetEnrollmentAsync(enrollmentDTO.EnrollmentId);
            if (enrollment != null)
            {
               var updatedEnrollment = enrollmentDTO.Adapt<Enrollment>();
                _unitOfWork.Enrollments.Update(enrollment);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}