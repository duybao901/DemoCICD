using DemoCICD.Domain.Entities;
using DemoCICD.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistence;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    // optional: <ApplicationDbContext>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {

    }

    // cấu hình các thực thể (entities) trong cơ sở dữ liệu khi Entity Framework Core khởi tạo mô hình dữ liệu (model)
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Dòng này yêu cầu EF Core tự động áp dụng các cấu hình từ các lớp cấu hình thực thể (IEntityTypeConfiguration)
        // được định nghĩa trong một assembly (tệp .dll)
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    // Identity
    public DbSet<AppUser> AppUses { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<ActionInFunction> ActionInFunctions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    // Entities
    public DbSet<Product> Products { get; set; }
}
