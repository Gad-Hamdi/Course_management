using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        [Required]
        public string PlanName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}