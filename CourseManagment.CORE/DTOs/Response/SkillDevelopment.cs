using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class SkillDevelopment
    {
        public string SkillCategory { get; set; } = null!;
        public int CoursesCompleted { get; set; }
        public decimal AverageScore { get; set; }
        public string ProficiencyLevel { get; set; } = null!;
    }
}
