using Microsoft.EntityFrameworkCore;

namespace lr12.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; } = null!;
        public DbSet<UserModel> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            Console.WriteLine(Database);
        }
    }
}
