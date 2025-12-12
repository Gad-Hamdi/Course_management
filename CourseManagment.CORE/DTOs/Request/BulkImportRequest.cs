using CourseManagment.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class BulkImportRequest
    {
        public List<ApplicationUser> Users { get; set; } = new();
        public string DefaultPassword { get; set; } = null!;
    }
}
