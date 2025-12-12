using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class StartQuizRequest
    {
        public string UserId { get; set; } = null!;
        public int QuizId { get; set; }
    }

}
