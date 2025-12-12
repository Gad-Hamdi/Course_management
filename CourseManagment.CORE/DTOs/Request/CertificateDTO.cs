using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class CertificateDTO
    {
        public string UserId { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string CertificateCode { get; set; } = null!;


        public string? DownloadUrl { get; set; }
    }
}
