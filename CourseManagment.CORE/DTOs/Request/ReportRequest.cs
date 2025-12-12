using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class ReportRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public Dictionary<string, object> Criteria { get; set; } = new();

        public int? CompanyId { get; set; }

        public string? Format { get; set; } = "json"; // json, csv, pdf
    }
}
