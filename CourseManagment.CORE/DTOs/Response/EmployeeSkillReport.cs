using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class EmployeeSkillReport
    {
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public List<Skill> Skills { get; set; } = new();
        public string ProficiencyLevel { get; set; } = null!;
        public List<KnowledgeGap> KnowledgeGaps { get; set; } = new();
        public List<RecommendedCourse> RecommendedCourses { get; set; } = new();
    }
}
