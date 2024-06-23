using Microsoft.EntityFrameworkCore;

namespace DemoAPIInterview.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options) { }
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Users> Users { get; set; } 
    }
}
