using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class RecommendedCourse
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public int MatchScore { get; set; }
    }
}
