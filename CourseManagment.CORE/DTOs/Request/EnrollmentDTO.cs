using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.DTOs.Request
{
    public class EnrollmentDTO
    {
        public int EnrollmentId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [Range(0, 100)]
        public decimal Progress { get; set; } = 0;

        public string Status { get; set; } = "Active";
    }
}