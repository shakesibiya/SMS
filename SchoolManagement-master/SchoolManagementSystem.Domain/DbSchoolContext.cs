using System.Data.Entity;
using SchoolManagementSystem.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SchoolManagementSystem.Domain
{
    public class DbSchoolContext : DbContext
    {
        public DbSchoolContext() : base("DbSchoolContext")
        {
            //Database.SetInitializer<DbSchoolContext>(null);

        }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostPictures> PostPicture { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ApplicationForm> Applications { get; set; }
        public DbSet<StudentTrush> Deregister { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventComment> EventComment { get; set; }
        public DbSet<SmsModel> Sms { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Voting> Votings { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<VotingGroup> VotingGroups { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<votingDetail> votingDetail { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}