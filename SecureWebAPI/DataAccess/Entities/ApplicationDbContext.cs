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
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<BacklogEntity> Backlogs { get; set; }
        public DbSet<XRefTeamUserEntity> xRefTeamsUsers { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<XRefBacklogTaskEntity> xRefBacklogTask { get; set; }
        public DbSet<XRefSprintTaskEntity> xRefSprintTask { get; set; }

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
            modelBuilder.Entity<TeamEntity>(ConfigureTeamEntity);
            modelBuilder.Entity<BacklogEntity>(ConfigureBacklogEntity);
            modelBuilder.Entity<XRefTeamUserEntity>(ConfigureXrefTeamUserEntity);
            modelBuilder.Entity<SprintEntity>(ConfigureSprintEntity);
            modelBuilder.Entity<XRefSprintTaskEntity>(ConfigureXrefSprintTaskEntity);
            modelBuilder.Entity<RoleEntity>(ConfigureRolekEntity);
            modelBuilder.Entity<XRefBacklogTaskEntity>(ConfigureXrefBacklogTaskEntity);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureXrefBacklogTaskEntity(EntityTypeBuilder<XRefBacklogTaskEntity> entity)
        {
            entity.ToTable("XRefBacklogTask");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TaskId).IsUnique();
            entity.Property(e => e.BacklogId);
            entity.Property(e => e.TaskId);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureRolekEntity(EntityTypeBuilder<RoleEntity> entity)
        {
            entity.ToTable("Role");
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureXrefSprintTaskEntity(EntityTypeBuilder<XRefSprintTaskEntity> entity)
        {
            entity.ToTable("XRefSprintTask");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TaskId).IsUnique();
            entity.Property(e => e.SprintId);
            entity.Property(e => e.TaskId);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureSprintEntity(EntityTypeBuilder<SprintEntity> entity)
        {
            entity.ToTable("Sprint");
            entity.HasKey(e => e.SprintId);
            entity.Property(e => e.SprintName);
            entity.HasIndex(e => e.SprintName).IsUnique();
            entity.Property(e => e.TeamId);
            entity.Property(e => e.StartDate).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.EndDate);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureXrefTeamUserEntity(EntityTypeBuilder<XRefTeamUserEntity> entity)
        {
            entity.ToTable("XRefTeamUser");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TeamId);
            entity.Property(e => e.UserId);
            entity.Property(e => e.RoleId);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureBacklogEntity(EntityTypeBuilder<BacklogEntity> entity)
        {
            entity.ToTable("Backlog");
            entity.HasKey(e => e.BacklogId);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureTeamEntity(EntityTypeBuilder<TeamEntity> entity)
        {
            entity.ToTable("Team");
            entity.HasKey(e => e.TeamId);
            entity.HasIndex(e => e.TeamName).IsUnique();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
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
            entity.Property(e => e.TeamId);
            entity.Property(e => e.Description);
            entity.Property(e => e.EffortId);
            entity.Property(e => e.PriorityId);
            entity.Property(e => e.Username);
            entity.Property(e => e.OrderId);
            entity.Property(e => e.Sprint);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}