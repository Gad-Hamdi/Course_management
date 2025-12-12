using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class CompanyProgressReport
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public List<CourseProgress> CourseProgress { get; set; } = new();
        public List<DepartmentProgress> DepartmentProgress { get; set; } = new();
        public decimal OverallProgress { get; set; }
    }
}
