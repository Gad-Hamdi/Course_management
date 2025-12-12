using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class NotificationPreferenceRequest
    {
        public bool EmailNotifications { get; set; } = true;
        public bool InAppNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = false;
        public bool CourseUpdates { get; set; } = true;
        public bool QuizResults { get; set; } = true;
        public bool AchievementAlerts { get; set; } = true;
        public bool SystemAnnouncements { get; set; } = true;
    }
}
