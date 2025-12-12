using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class WebhookDelivery
    {
        public int WebhookDeliveryId { get; set; }
        public int WebhookId { get; set; }
        public string EventType { get; set; } = null!;
        public string Payload { get; set; } = null!;
        public string? Response { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime DeliveredAt { get; set; } = DateTime.UtcNow;
        public TimeSpan? ResponseTime { get; set; }
        public Webhook Webhook { get; set; } = null!;
    }
}
