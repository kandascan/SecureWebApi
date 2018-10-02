using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; } 
        public DbSet<TodoEntity> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoEntity>(ConfigureTodoEntity);
        }
        
        private void ConfigureTodoEntity(EntityTypeBuilder<TodoEntity> entity)
        {
            entity.ToTable("Todo");
            entity.HasKey(e => e.Id);
            entity
            .HasOne(e => e.User)
            .WithMany(c => c.Todos);
            entity.Property(e => e.Name);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}