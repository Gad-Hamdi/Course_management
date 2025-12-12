using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class GamificationActivity
    {
        public int PointsLast30Days { get; set; }
        public int BadgesLast30Days { get; set; }
        public string? MostAwardedBadge { get; set; }
    }
}
