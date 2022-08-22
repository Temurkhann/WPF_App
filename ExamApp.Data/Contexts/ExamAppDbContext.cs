using ExamApp.Domain.Entities.Attachments;
using ExamApp.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data.Contexts
{
    public class ExamAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Database=ExamApp;Username=postgres;Password=8520";
            optionsBuilder.UseNpgsql(connectionString);

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
    }
}
