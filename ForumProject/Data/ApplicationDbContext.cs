using ForumProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectForum.Models;

namespace ForumProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<ForumPost> Posts { get; set; }

        public virtual DbSet<SiteUser>  Users { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ForumPost>()
                .HasOne(t => t.Owner)
                .WithMany(t => t.Posts)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ForumPost>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.ForumPost)
                .HasForeignKey(t => t.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(t => t.Owner)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
           
            builder.Entity<SiteUser>()
                .HasMany(t => t.Posts)
                .WithOne(t =>t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SiteUser>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}