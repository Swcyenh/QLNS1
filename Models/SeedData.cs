using Microsoft.AspNetCore.Identity;
using QLNS1.Models;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User", "Manager" };

        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            // Check if the role exists
            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                // Create the roles and seed them to the database
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        User user = await userManager.FindByNameAsync("Shensan12");

        if (user == null)
        {
            user = new User()
            {
                UserName = "Swcyen",
                Email = "admin@Swcyen.com",
                FirstName = "Swcyen",
                LastName = "Constatine"

            };
            await userManager.CreateAsync(user, "Admin123!");
        }

        // Assign the admin user to the admin role
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
