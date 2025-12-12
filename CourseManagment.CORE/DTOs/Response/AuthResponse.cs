using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public UserResponse User { get; set; } = null!;
    }
}
