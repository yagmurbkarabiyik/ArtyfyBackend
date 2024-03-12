using ArtyfyBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Dal.Context
{
    public class ArtyfyBackendDbContext : IdentityDbContext
    {
        public ArtyfyBackendDbContext(DbContextOptions<ArtyfyBackendDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=YAGMUR;database=ArtyfyBackendDb;integrated security=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<UserSavedPost> UserSavedPosts { get; set; }
    }
}