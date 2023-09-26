using MassData.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MassData.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Salary> Salaries { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}