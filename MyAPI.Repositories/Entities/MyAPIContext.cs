using Microsoft.EntityFrameworkCore;

namespace MyAPI.Repositories.Entities
{
    public partial class MyAPIContext : DbContext
    {
        public MyAPIContext(DbContextOptions<MyAPIContext> options)
            : base(options)
        {
        }

        // Add your DbSets here
        public virtual DbSet<User> Users { get; set; }
        // Example:
        // public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("Users");
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.PasswordHash).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
