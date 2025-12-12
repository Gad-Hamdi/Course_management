using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class Skill
    {
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Proficiency { get; set; } = null!;
        public DateTime LastUsed { get; set; }
    }
}
