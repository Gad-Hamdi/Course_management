using CourseManagment.CORE.Identity;
using CourseManagment.CORE.Interfaces;
using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CourseManagment.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Badge> Badges { get; set; } = null!;
        public DbSet<Certificate> Certificates { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<EmployeeBadge> EmployeeBadges { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<PointTransaction> PointTransactions { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<CertificateTemplate> CertificateTemplates { get; set; } = null!;
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; } = null!;
        public DbSet<Webhook> Webhooks { get; set; } = null!;
        public DbSet<WebhookDelivery> WebhookDeliveries { get; set; } = null!;
      //  public DbSet<Integration> Integrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<CertificateTemplate>().ToTable("CertificateTemplates");
            modelBuilder.Entity<LeaderboardEntry>().ToTable("LeaderboardEntries");

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.JobTitle).HasMaxLength(100);
                entity.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(u => u.IsActive).HasDefaultValue(true);
                entity.Property(u => u.Points).HasDefaultValue(0);

                // CompanyId is required
                entity.Property(u => u.CompanyId).IsRequired();

                entity.HasIndex(u => u.CompanyId);
                entity.HasIndex(u => new { u.FirstName, u.LastName });
            });

            // Configure relationships

            // Company -> Subscription (One-to-One)
            modelBuilder.Entity<Company>()
                .HasOne(c => c.Subscription)
                .WithMany()
                .HasForeignKey(c => c.SubscriptionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Company -> ApplicationUser (One-to-Many) - CHANGED to Restrict
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(u => u.CompanyId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); // Changed from SetNull to Restrict

            // Company -> Course (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Company)
                .WithMany(co => co.Courses)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Lesson (One-to-Many)
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course -> Quiz (One-to-Many)
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Course)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(q => q.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course -> Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Certificate (One-to-Many)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Course)
                .WithMany(co => co.Certificates)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser -> Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> QuizAttempt (One-to-Many)
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(qa => qa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> Certificate (One-to-Many)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> Notification (One-to-Many)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> PointTransaction (One-to-Many)
            modelBuilder.Entity<PointTransaction>()
                .HasOne(pt => pt.User)
                .WithMany(u => u.PointTransactions)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> EmployeeBadge (One-to-Many)
            modelBuilder.Entity<EmployeeBadge>()
                .HasOne(eb => eb.User)
                .WithMany(u => u.EmployeeBadges)
                .HasForeignKey(eb => eb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Badge -> EmployeeBadge (One-to-Many)
            modelBuilder.Entity<EmployeeBadge>()
                .HasOne(eb => eb.Badge)
                .WithMany(b => b.EmployeeBadges)
                .HasForeignKey(eb => eb.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quiz -> Question (One-to-Many)
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qu => qu.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz -> QuizAttempt (One-to-Many)
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(q => q.QuizAttempts)
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints
            modelBuilder.Entity<EmployeeBadge>()
                .HasIndex(eb => new { eb.UserId, eb.BadgeId })
                .IsUnique();

            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.UserId, e.CourseId })
                .IsUnique();

            modelBuilder.Entity<Certificate>()
                .HasIndex(c => c.CertificateCode)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<CertificateTemplate>()
                .HasIndex(ct => ct.Name)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => new { c.CompanyId, c.Title })
                .IsUnique();

            modelBuilder.Entity<Badge>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.OrderIndex })
                .IsUnique();

            // Configure entities
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.CompanyId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(c => c.SubscriptionId).IsRequired(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Description).HasMaxLength(2000);
                entity.Property(c => c.Category).HasMaxLength(100);
                entity.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(c => c.Duration).HasDefaultValue(0);
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(q => q.QuizId);
                entity.Property(q => q.Title).IsRequired().HasMaxLength(200);
                entity.Property(q => q.PassingScore).HasDefaultValue(70);
                entity.Property(q => q.Duration).HasDefaultValue(30);
                entity.Property(q => q.IsPublished).HasDefaultValue(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.QuestionId);
                entity.Property(q => q.QuestionText).IsRequired().HasMaxLength(1000);
                entity.Property(q => q.Type).IsRequired().HasMaxLength(50).HasDefaultValue("MultipleChoice");
                entity.Property(q => q.Options).HasMaxLength(2000);
                entity.Property(q => q.CorrectAnswer).HasMaxLength(500);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Active");
                entity.Property(e => e.Progress).HasPrecision(5, 2).HasDefaultValue(0);
                entity.Property(e => e.EnrolledAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.HasCheckConstraint("CK_Enrollment_Progress", "[Progress] >= 0 AND [Progress] <= 100");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.HasKey(c => c.CertificateId);
                entity.Property(c => c.CertificateCode).IsRequired().HasMaxLength(100);
                entity.Property(c => c.DownloadUrl).HasMaxLength(500);
                entity.Property(c => c.GeneratedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.HasKey(b => b.BadgeId);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<PointTransaction>(entity =>
            {
                entity.HasKey(pt => pt.PointTransactionId);
                entity.Property(pt => pt.Reason).HasMaxLength(200);
                entity.Property(pt => pt.TransactionDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(pt => pt.Points).IsRequired();
                entity.HasCheckConstraint("CK_PointTransaction_Points", "[Points] != 0");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationId);
                entity.Property(n => n.Message).IsRequired().HasMaxLength(1000);
                entity.Property(n => n.Type).HasMaxLength(50).HasDefaultValue("System");
                entity.Property(n => n.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(n => n.IsRead).HasDefaultValue(false);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(l => l.LessonId);
                entity.Property(l => l.Title).IsRequired().HasMaxLength(200);
                entity.Property(l => l.ContentUrl).HasMaxLength(500);
                entity.Property(l => l.OrderIndex).IsRequired(false);
                entity.Property(l => l.Duration).HasDefaultValue(0);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.SubscriptionId);
                entity.Property(s => s.PlanName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.StartDate).IsRequired();
                entity.Property(s => s.EndDate).IsRequired();
                entity.Property(s => s.IsActive).HasDefaultValue(true);
                entity.HasCheckConstraint("CK_Subscription_Dates", "[EndDate] > [StartDate]");
            });

            modelBuilder.Entity<QuizAttempt>(entity =>
            {
                entity.HasKey(qa => qa.QuizAttemptId);
                entity.Property(qa => qa.AttemptDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(qa => qa.Passed).HasDefaultValue(false);
                entity.Property(qa => qa.Score).HasDefaultValue(0);
                entity.HasCheckConstraint("CK_QuizAttempt_Score", "[Score] >= 0 AND [Score] <= 100");
            });

            modelBuilder.Entity<EmployeeBadge>(entity =>
            {
                entity.HasKey(eb => eb.Id);
                entity.Property(eb => eb.AwardedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<CertificateTemplate>(entity =>
            {
                entity.HasKey(ct => ct.Id);
                entity.Property(ct => ct.Name).IsRequired().HasMaxLength(100);
                entity.Property(ct => ct.TemplateHtml).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(ct => ct.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<LeaderboardEntry>(entity =>
            {
                entity.HasKey(le => le.leaderboardEntryId);
                entity.Property(le => le.UserId).IsRequired().HasMaxLength(450);
                entity.Property(le => le.UserName).IsRequired().HasMaxLength(200);
                entity.Property(le => le.Points).HasDefaultValue(0);
                entity.Property(le => le.Rank).HasDefaultValue(0);
                entity.Property(le => le.BadgesCount).HasDefaultValue(0);
                entity.Property(le => le.CompletedCourses).HasDefaultValue(0);
            });
            modelBuilder.Entity<Webhook>(entity =>
            {
                entity.HasKey(w => w.WebhookId);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
                entity.Property(w => w.TargetUrl).IsRequired().HasMaxLength(500);
                entity.Property(w => w.Secret).IsRequired().HasMaxLength(100);
                entity.Property(w => w.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(w => w.IsActive).HasDefaultValue(true);

                // Store events as JSON array
                entity.Property(w => w.Events)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.HasOne(w => w.Company)
                    .WithMany()
                    .HasForeignKey(w => w.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WebhookDelivery>(entity =>
            {
                entity.HasKey(wd => wd.WebhookDeliveryId);
                entity.Property(wd => wd.EventType).IsRequired().HasMaxLength(50);
                entity.Property(wd => wd.Payload).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(wd => wd.Response).HasColumnType("nvarchar(max)");
                entity.Property(wd => wd.DeliveredAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(wd => wd.Webhook)
                    .WithMany(w => w.Deliveries)
                    .HasForeignKey(wd => wd.WebhookId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

           /* modelBuilder.Entity<Integration>(entity =>
            {
                entity.HasKey(i => i.IntegrationId);
                entity.Property(i => i.Name).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Type).IsRequired().HasMaxLength(50);
                entity.Property(i => i.ApiKey).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Config).HasColumnType("nvarchar(max)");
                entity.Property(i => i.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(i => i.IsActive).HasDefaultValue(true);

                entity.HasOne(i => i.Company)
                    .WithMany()
                    .HasForeignKey(i => i.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });*/
        }
    }
    }
