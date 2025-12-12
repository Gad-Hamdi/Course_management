using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class SubmitQuizRequest
    {
        public Dictionary<int, string> Answers { get; set; } = new();
    }
}
