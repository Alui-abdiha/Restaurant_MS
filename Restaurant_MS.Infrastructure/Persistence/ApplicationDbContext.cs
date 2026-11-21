
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant_MS.Domain.Entities;
using Restaurant_MS.Infrastructure.Identity;

namespace Restaurant_MS.Infrastructure.Persistence;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Product Management
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    // Permission Management
    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCategory(modelBuilder);

        ConfigureProduct(modelBuilder);

        ConfigurePermission(modelBuilder);

        ConfigureRolePermission(modelBuilder);
    }

    private static void ConfigureCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Description)
                .HasMaxLength(500);

            entity.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        });
    }

    private static void ConfigureProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Description)
                .HasMaxLength(500);

            entity.Property(x => x.Price)
                .HasPrecision(18, 2);

            entity.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurePermission(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permissions");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasIndex(x => x.Code)
                .IsUnique();

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Module)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Description)
                .HasMaxLength(500);
        });
    }

    private static void ConfigureRolePermission(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermissions");

            entity.HasKey(x => new
            {
                x.RoleId,
                x.PermissionId
            });

            entity.HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
