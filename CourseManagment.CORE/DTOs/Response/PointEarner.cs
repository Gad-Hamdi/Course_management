using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class PointEarner
    {
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public int Points { get; set; }
        public int BadgesCount { get; set; }
    }
}
