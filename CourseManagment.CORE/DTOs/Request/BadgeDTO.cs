using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class BadgeDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
