using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureWebAPI.Models;

namespace SecureWebAPI.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } 
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>(ConfigureUserEntity);
            modelBuilder.Entity<Todo>(ConfigureTodoEntity);
        }

        // private void ConfigureUserEntity(EntityTypeBuilder<User> entity)
        // {
        //     entity.hask(e => e.Id);
        //     entity
        //     .HasMany(c => c.Todos)
        //     .WithOne(e => e.User)
        //     .HasForeignKey(p => p.UserId);
        // }
        
        private void ConfigureTodoEntity(EntityTypeBuilder<Todo> entity)
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