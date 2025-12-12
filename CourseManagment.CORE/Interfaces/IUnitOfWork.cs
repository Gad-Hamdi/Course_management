using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;

public interface IUnitOfWork : IDisposable
{
    IRepository<Badge> Badges { get; }
    IRepository<Certificate> Certificates { get; }
    IRepository<Company> Companies { get; }
    IRepository<Course> Courses { get; }
    IRepository<EmployeeBadge> EmployeeBadges { get; }
    IRepository<Enrollment> Enrollments { get; }
    IRepository<Lesson> Lessons { get; }
    IRepository<Notification> Notifications { get; }
    IRepository<PointTransaction> PointTransactions { get; }
    IRepository<Question> Questions { get; }
    IRepository<Quiz> Quizzes { get; }
    IRepository<QuizAttempt> QuizAttempts { get; }
    IRepository<Subscription> Subscriptions { get; }
    IRepository<LeaderboardEntry> LeaderboardEntries { get; }
    IRepository<CertificateTemplate> CertificateTemplates { get; }
    IRepository<Webhook> Webhooks { get; }
    IRepository<WebhookDelivery> WebhookDeliveries { get; }
   // IRepository<Integration> Integrations { get; }
    IRepository<Report> Reports { get; }

    int Complete();
    Task<int> CompleteAsync();
}