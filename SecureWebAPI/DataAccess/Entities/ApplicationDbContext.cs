using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SecureWebAPI.DataAccess.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = "Data Source=(local);Initial Catalog=SM;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>(ConfigureTaskEntity);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureTaskEntity(EntityTypeBuilder<TaskEntity> entity)
        {
            entity.ToTable("Task");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Taskname);
            entity.Property(e => e.Description);
            entity.Property(e => e.Effort);
            entity.Property(e => e.Priority);
            entity.Property(e => e.Username);
            entity.Property(e => e.OrderId);
            entity.Property(e => e.BacklogItem);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}