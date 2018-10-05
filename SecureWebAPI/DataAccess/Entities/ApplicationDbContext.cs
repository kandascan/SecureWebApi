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
        public DbSet<TodoEntity> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = "Data Source=DESKTOP-BVKHG2F;Initial Catalog=Todo;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoEntity>(ConfigureTodoEntity);
            base.OnModelCreating(modelBuilder);
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