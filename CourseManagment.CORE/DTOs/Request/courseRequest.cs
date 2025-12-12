using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class courseRequest
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Category { get; set; }

        public int? Duration { get; set; }

    }
}
