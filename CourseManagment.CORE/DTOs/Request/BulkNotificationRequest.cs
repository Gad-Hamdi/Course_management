using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class BulkNotificationRequest
    {
        public List<string> UserIds { get; set; } = new();
        public string Message { get; set; } = null!;
        public string Type { get; set; } = "System";
        public string? Subject { get; set; }
    }
}
