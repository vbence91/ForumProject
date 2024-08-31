using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectForum.Models;

namespace ForumProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ForumPost> Posts { get; set; }

        public DbSet<SiteUser>  Users { get; set; }

        public DbSet<ForumPostComment> Comments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ForumPost>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // TODO ForumPost - ForumPostComment many to many
                

            base.OnModelCreating(builder);
        }
    }
}