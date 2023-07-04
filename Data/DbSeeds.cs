using Microsoft.AspNetCore.Identity;

namespace liquidador_web.Data
{
    public class DbSeeds
    {
        public static void RoleSeeder(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Administrador General", NormalizedName = "ADMIN".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Administrador Seccional", NormalizedName = "ADMIN_SEC".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Administrador de Actualización de las Tasas", NormalizedName = "ADMIN_TASAS".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Usuario Liquidador", NormalizedName = "LIQUID_USER".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Liquidador Definitivo", NormalizedName = "JUEZ".ToUpper() });
        }

        public static void UserSeeder(UserManager<IdentityUser> userManager) {
            if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "ADMIN").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }
    }
}