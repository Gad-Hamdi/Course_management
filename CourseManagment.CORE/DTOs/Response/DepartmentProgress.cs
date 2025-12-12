using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class DepartmentProgress
    {
        public string DepartmentName { get; set; } = null!;
        public int EmployeeCount { get; set; }
        public decimal AverageProgress { get; set; }
        public int CompletedCourses { get; set; }
    }
}
