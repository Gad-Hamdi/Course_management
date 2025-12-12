using CourseManagment.CORE.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseManagment.CORE.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public int? SubscriptionId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Subscription? Subscription { get; set; }
        public ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();
        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
    }
}