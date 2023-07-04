using liquidador_web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace liquidador_web.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await RolesSeeder(roleManager);

            var userManager = services.GetRequiredService<UserManager<LiquidadorUser>>();
            await UsersSeeders(userManager);
        }

        public static async Task RolesSeeder(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync("Administrador General") &&
                                await roleManager.RoleExistsAsync("Administrador de Actualización de las Tasas") &&
                                await roleManager.RoleExistsAsync("Administrador Seccional") &&
                                await roleManager.RoleExistsAsync("Usuario Liquidador") &&
                                await roleManager.RoleExistsAsync("Juez");

            if (alreadyExists)
                return;

            await roleManager.CreateAsync(new IdentityRole("Administrador General"));
            await roleManager.CreateAsync(new IdentityRole("Administrador de Actualización de las Tasas"));
            await roleManager.CreateAsync(new IdentityRole("Administrador Seccional"));
            await roleManager.CreateAsync(new IdentityRole("Usuario Liquidador"));
            await roleManager.CreateAsync(new IdentityRole("Juez"));
        }

        private static async Task UsersSeeders(UserManager<LiquidadorUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "admin@ramajudicial.gov.co").SingleOrDefaultAsync();
            var testUser = await userManager.Users.Where(x => x.UserName == "user@ramajudicial.gov.co").SingleOrDefaultAsync();
            var testAdminSecc = await userManager.Users.Where(x => x.UserName == "seccional@ramajudicial.gov.co").SingleOrDefaultAsync();
            var testAdminTasas = await userManager.Users.Where(x => x.UserName == "tasas@ramajudicial.gov.co").SingleOrDefaultAsync();
            var testJuez = await userManager.Users.Where(x => x.UserName == "juez@ramajudicial.gov.co").SingleOrDefaultAsync();

            if (testAdmin == null) {
                testAdmin = new LiquidadorUser
                {
                    UserName = "admin@ramajudicial.gov.co",
                    Email = "admin@ramajudicial.gov.co",
                    EmailConfirmed = true,
                    FullName = "Administrador"
                };

                await userManager.CreateAsync(testAdmin, "Prueba-0");
                await userManager.AddToRoleAsync(testAdmin, "Administrador General");
            }

            if (testUser == null) {
                testUser = new LiquidadorUser
                {
                    UserName = "user@ramajudicial.gov.co",
                    Email = "user@ramajudicial.gov.co",
                    EmailConfirmed = true,
                    FullName = "Usuario"
                };

                await userManager.CreateAsync(testUser, "Prueba-0");
                await userManager.AddToRoleAsync(testUser, "Usuario Liquidador");
            }

            if (testAdminSecc == null) {
                testAdminSecc = new LiquidadorUser
                {
                    UserName = "seccional@ramajudicial.gov.co",
                    Email = "seccional@ramajudicial.gov.co",
                    EmailConfirmed = true,
                    FullName = "Seccional"
                };

                await userManager.CreateAsync(testAdminSecc, "Prueba-0");
                await userManager.AddToRoleAsync(testAdminSecc, "Administrador Seccional");
            }

            if (testAdminTasas == null) {
                testAdminTasas = new LiquidadorUser
                {
                    UserName = "tasas@ramajudicial.gov.co",
                    Email = "tasas@ramajudicial.gov.co",
                    EmailConfirmed = true,
                    FullName = "Tasas"
                };

                await userManager.CreateAsync(testAdminTasas, "Prueba-0");
                await userManager.AddToRoleAsync(testAdminTasas, "Administrador de Actualización de las Tasas");
            }

            if (testJuez == null)
            {
                testJuez = new LiquidadorUser
                {
                    UserName = "juez@ramajudicial.gov.co",
                    Email = "juez@ramajudicial.gov.co",
                    EmailConfirmed = true,
                    FullName = "Juez"
                };

                await userManager.CreateAsync(testJuez, "Prueba-0");
                await userManager.AddToRoleAsync(testJuez, "Juez");
            }
        }
    }
}