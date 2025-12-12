using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
   
    public class KnowledgeGap
    {
        public string Area { get; set; } = null!;
        public string WeaknessLevel { get; set; } = null!;
        public string RecommendedAction { get; set; } = null!;
    }
}
