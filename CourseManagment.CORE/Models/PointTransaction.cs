using CourseManagment.CORE.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class PointTransaction
    {
        public int PointTransactionId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int Points { get; set; }

        public string? Reason { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
    }
}
