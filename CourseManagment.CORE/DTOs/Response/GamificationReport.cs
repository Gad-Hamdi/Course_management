using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class GamificationReport
    {
        public int CompanyId { get; set; }
        public int TotalPointsAwarded { get; set; }
        public int TotalBadgesAwarded { get; set; }
        public decimal AveragePointsPerEmployee { get; set; }
        public List<PointEarner> TopPointEarners { get; set; } = new();
        public GamificationActivity RecentActivity { get; set; } = new();
    }
}
