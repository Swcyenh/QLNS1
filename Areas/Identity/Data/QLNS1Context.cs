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
        
    }
    public DbSet<QLNS1.Models.Sach> Sach { get; set; }
    public DbSet<QLNS1.Models.NhapSach> Nhap { get; set; }
    public DbSet<QLNS1.Models.Invoice> Invoice { get; set; }
    public DbSet<QLNS1.Models.ThuTien> ThuTien { get; set; }
}
