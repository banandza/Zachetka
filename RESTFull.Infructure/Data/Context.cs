using Microsoft.EntityFrameworkCore;
using RESTFull.Domain.Model;

namespace RESTFull.Infrastructure.data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<CourseLeader> CourseLeaders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseLeader>()
                .HasKey(cl => new { cl.TeacherId, cl.DisciplineId });
            modelBuilder.Entity<CourseLeader>()
                .HasOne(t => t.Teacher)
                .WithMany(cl => cl.CourseLeaders)
                .HasForeignKey(t => t.TeacherId);
            modelBuilder.Entity<CourseLeader>()
                .HasOne(d => d.Discipline)
                .WithMany(cl => cl.CourseLeaders)
                .HasForeignKey(d => d.DisciplineId);
        }
    }
}