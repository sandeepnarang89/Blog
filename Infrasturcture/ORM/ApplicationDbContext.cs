using Blogpost.Core.Application;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Blogpost.Core.User.Domain;
using Blogpost.Core.Blogs.Domain;

namespace Blogpost.Infrasturcture.ORM
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(ISettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        public void InitDb()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public virtual DbSet<Person> Person { get; set; }
        
        public virtual DbSet<UserLoginLog> UserLoginLog { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
    }
}
