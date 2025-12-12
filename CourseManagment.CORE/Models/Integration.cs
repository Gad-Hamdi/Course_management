using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class Integration
    {
        public int IntegrationId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public Dictionary<string, object> Config { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Company Company { get; set; } = null!;
    }
}
