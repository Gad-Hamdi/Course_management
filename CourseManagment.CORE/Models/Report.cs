using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string Criteria { get; set; } = null!;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string? DownloadUrl { get; set; }
        public int? CompanyId { get; set; }
        public string? GeneratedBy { get; set; }
        public Company? Company { get; set; }
    }
}
