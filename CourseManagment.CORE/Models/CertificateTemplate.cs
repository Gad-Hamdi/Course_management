using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.Models
{
    public class CertificateTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TemplateHtml { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
