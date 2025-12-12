using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class ReportResponse
    {
        public int ReportId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string? DownloadUrl { get; set; }
        public string Status { get; set; } = "Completed";
        public Dictionary<string, object> Data { get; set; } = new();
    }
}
