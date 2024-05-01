﻿using ArtyfyBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Dal.Context
{
	public class ArtyfyBackendDbContext : IdentityDbContext
	{
		public ArtyfyBackendDbContext(DbContextOptions<ArtyfyBackendDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserLikedPost>()
			  .HasOne(up => up.Post)
			  .WithMany(p => p.UserLikedPosts)
			  .HasForeignKey(up => up.PostId)
			  .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UserSavedPost>()
			  .HasOne(up => up.Post)
			  .WithMany(p => p.UserSavedPosts)
			  .HasForeignKey(up => up.PostId)
			  .OnDelete(DeleteBehavior.NoAction);
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<UserSavedPost> UserSavedPosts { get; set; }
		public DbSet<UserLikedPost> UserLikedPosts { get; set; }
	}
}