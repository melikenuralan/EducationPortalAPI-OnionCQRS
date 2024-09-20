using EducationPortal.Domain;
using EducationPortal.Domain.Entities;
using EducationPortal.Domain.Entities.Common;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;


namespace EducationPortal.Persistance.Contexts
{

    public class EduPortalDbContext : IdentityDbContext<User, Role, Guid>
    {
        public EduPortalDbContext() { }
        public EduPortalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ReferenceCode> ReferenceCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>()
                   .HasOne(c => c.Category)
                   .WithMany(c => c.Courses)
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<UserCourse>()
                   .HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.Entity<Category>()
                   .Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Entity<User>()
               .HasOne(u => u.Team)
               .WithMany(t => t.Users)
               .HasForeignKey(u => u.TeamId)
               .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<ReferenceCode>()
                .HasOne(rc => rc.Team)
                .WithMany(t => t.ReferenceCodes)
                .HasForeignKey(rc => rc.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Team>()
                .HasOne(t => t.User)
                .WithMany() 
                .HasForeignKey(t => t.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            var datas = ChangeTracker
                .Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    EntityState.Deleted => data.Entity.DeletedDate = DateTime.Now,
                    _ => DateTime.Now,
                };
            }
            return await base.SaveChangesAsync(cancellation);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}

