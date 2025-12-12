using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using CourseManagment.EF.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace CourseManagment.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Badges = new Repositories<Badge>(_context);
            Certificates = new Repositories<Certificate>(_context);
            Companies = new Repositories<Company>(_context);
            Courses = new Repositories<Course>(_context);
            EmployeeBadges = new Repositories<EmployeeBadge>(_context);
            Enrollments = new Repositories<Enrollment>(_context);
            Lessons = new Repositories<Lesson>(_context);
            Notifications = new Repositories<Notification>(_context);
            PointTransactions = new Repositories<PointTransaction>(_context);
            Questions = new Repositories<Question>(_context);
            Quizzes = new Repositories<Quiz>(_context);
            QuizAttempts = new Repositories<QuizAttempt>(_context);
            
            Subscriptions = new Repositories<Subscription>(_context);
            LeaderboardEntrys = new Repositories<LeaderboardEntry>(_context);
            certificateTemplates = new Repositories<CertificateTemplate>(_context);
            Webhooks = new Repositories<Webhook>(_context);
            WebhookDeliveries = new Repositories<WebhookDelivery>(_context);
            //Integrations = new Repositories<Integration>(_context);

        }

        public IRepository<Badge> Badges { get; }
        public IRepository<Certificate> Certificates { get; }
        public IRepository<Company> Companies { get; }
        public IRepository<Course> Courses { get; }
        public IRepository<EmployeeBadge> EmployeeBadges { get; }//kjhgf
        public IRepository<Enrollment> Enrollments { get; }
        public IRepository<Lesson> Lessons { get; }//lkjhgf
        public IRepository<Notification> Notifications { get; }//xxxxxxxxxxxxx
        public IRepository<PointTransaction> PointTransactions { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<Quiz> Quizzes { get; }
        public IRepository<QuizAttempt> QuizAttempts { get; }
        public IRepository<Subscription> Subscriptions { get; }//jhgfd
        public IRepository<LeaderboardEntry> LeaderboardEntrys { get; }
        public IRepository<CertificateTemplate > certificateTemplates { get; }
        public IRepository<Webhook> Webhooks { get; }
        public IRepository<WebhookDelivery> WebhookDeliveries { get; }
        //public IRepository<Integration> Integrations { get; }

        public IRepository<LeaderboardEntry> LeaderboardEntries { get; }
        public IRepository<CertificateTemplate> CertificateTemplates { get; }
        public IRepository<Report> Reports { get; }


        public int Complete() => _context.SaveChanges();
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

       
    }
}