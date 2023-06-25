using Linq1.Models;
using Microsoft.EntityFrameworkCore;

namespace Linq1.Data
{
    public class ClubDb : DbContext
    {
        public ClubDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Club> Clubs{ get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments{ get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Instructor> Instructors{ get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
