using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLNS1.Data;
using QLNS1.Models;
using QLNS1.Areas.Identity;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("QLNS1ContextConnection") ?? throw new InvalidOperationException("Connection string 'QLNS1ContextConnection' not found.");

builder.Services.AddDbContext<QLNS1Context>(options =>
    options.UseSqlServer(connectionString,builder=> {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<QLNS1Context>();
    

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
