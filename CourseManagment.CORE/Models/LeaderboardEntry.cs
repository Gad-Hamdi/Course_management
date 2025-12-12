namespace CourseManagment.CORE.Models
{
    public class LeaderboardEntry
    {
        public int leaderboardEntryId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int Points { get; set; }
        public int Rank { get; set; }
        public int BadgesCount { get; set; }
        public int CompletedCourses { get; set; }
    }
}