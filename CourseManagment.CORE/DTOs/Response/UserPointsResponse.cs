using CourseManagment.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class UserPointsResponse
    {
        public int TotalPoints { get; set; }
        public List<PointTransaction> Transactions { get; set; } = new();
    }
}
