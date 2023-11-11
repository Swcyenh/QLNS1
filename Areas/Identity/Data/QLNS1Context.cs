using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLNS1.Models;
namespace QLNS1.Data;

public class QLNS1Context : IdentityDbContext<User>
{
    public QLNS1Context(DbContextOptions<QLNS1Context> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<NhapSach>().HasKey(o => new { o.SachId, o.Id });
    }
    public DbSet<QLNS1.Models.Sach> Sach { get; set; }
    public DbSet<QLNS1.Models.NhapSach> Nhap { get; set; }
}
