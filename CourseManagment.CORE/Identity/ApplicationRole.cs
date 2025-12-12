using Microsoft.AspNetCore.Identity;

namespace CourseManagment.CORE.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}