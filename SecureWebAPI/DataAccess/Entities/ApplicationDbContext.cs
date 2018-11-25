using System;
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
        public DbSet<PriorityEntity> Priorities { get; set; }
        public DbSet<EffortEntity> Efforts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = "Data Source=(local);Initial Catalog=SM;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>(ConfigureTaskEntity);
            modelBuilder.Entity<PriorityEntity>(ConfigurePrioritykEntity);
            modelBuilder.Entity<EffortEntity>(ConfigureEffortkEntity);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureEffortkEntity(EntityTypeBuilder<EffortEntity> entity)
        {
            entity.ToTable("Effort");
            entity.HasKey(e => e.EffortId);
            entity.Property(e => e.EffortId).ValueGeneratedNever();
            entity.Property(e => e.EffortName);
        }

        private void ConfigurePrioritykEntity(EntityTypeBuilder<PriorityEntity> entity)
        {
            entity.ToTable("Priority");
            entity.HasKey(e => e.PriorityId);
            entity.Property(e => e.PriorityId).ValueGeneratedNever();
            entity.Property(e => e.PriorityName);
        }

        private void ConfigureTaskEntity(EntityTypeBuilder<TaskEntity> entity)
        {
            entity.ToTable("Task");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TaskName);
            entity.Property(e => e.Description);
            entity.Property(e => e.EffortId);
            entity.Property(e => e.PriorityId);
            entity.Property(e => e.Username);
            entity.Property(e => e.OrderId);
            entity.Property(e => e.BacklogItem);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}