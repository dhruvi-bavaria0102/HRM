
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Data.Models
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        private readonly DbContextOptions _options;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Employee> Employee { get; set; }
        //public DbSet<LoginUser> LoginUser { get; set; }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
        {
            public ApplicationDBContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../HRM/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<ApplicationDBContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new ApplicationDBContext(builder.Options);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
