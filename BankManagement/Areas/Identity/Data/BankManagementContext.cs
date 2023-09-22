using BankManagement.Areas.Identity.Data;
using BankManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Data;

public class BankManagementContext : IdentityDbContext<BankUser>
{
    public BankManagementContext(DbContextOptions<BankManagementContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<BankUser> BankUsers { get; set; } = default!;
    public DbSet<Account> Accounts { get; set; } = default!;
}
