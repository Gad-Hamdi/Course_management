using CourseManagment.CORE.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string CertificateCode { get; set; } = null!;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public string? DownloadUrl { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}