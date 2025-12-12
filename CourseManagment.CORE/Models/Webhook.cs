using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class Webhook
    {
        public int WebhookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string TargetUrl { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public List<string> Events { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastTriggeredAt { get; set; }
        public Company Company { get; set; } = null!;
        public List<WebhookDelivery> Deliveries { get; set; } = new();
    }


}
