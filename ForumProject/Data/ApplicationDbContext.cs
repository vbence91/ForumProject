﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                .WithMany(t => t.Posts)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ForumPost>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.ForumPost)
                .HasForeignKey(t => t.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ForumPostComment>()
                .HasOne(t => t.Owner)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(builder);
        }
    }
}