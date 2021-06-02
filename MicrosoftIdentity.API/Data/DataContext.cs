using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, AppRoles, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // modelBuilder.Entity<AppRoles>().ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);
 
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);
            base.OnModelCreating(modelBuilder);
        }
    }
}