using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class AwardBadgeRequest
    {
        public string UserId { get; set; } = null!;
        public int BadgeId { get; set; }
    }
}
