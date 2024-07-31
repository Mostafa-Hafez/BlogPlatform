using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Models
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext()
        {
         }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Follower>()
                .HasKey(z => new {z.UserId,z.FollowerID});
            modelBuilder.Entity<Follower>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }


    }
}
