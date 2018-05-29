using KolaNaukowe.web.Authorization;
using KolaNaukowe.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new KolaNaukoweDbContext(serviceProvider.GetRequiredService<DbContextOptions<KolaNaukoweDbContext>>()))
            {
                context.Database.EnsureCreated();
                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@test.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorRole);
                
                var leaderID = await EnsureUser(serviceProvider, testUserPw, "leader@test.com");
                await EnsureRole(serviceProvider, leaderID, Constants.LeaderRole);
                
                var userID = await EnsureUser(serviceProvider, testUserPw, "user@test.com");
                await EnsureRole(serviceProvider, userID, Constants.UserRole);
                SeedDb(context, leaderID);
            }

        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = UserName };
                IdentityResult x = await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDb(KolaNaukoweDbContext context, string leaderID)
        {
            if (context.StudentResearchGroups.Any())
            {
                return;   // DB has been seeded
            }

            var studentGroups = new StudentResearchGroup[]
            {
            new StudentResearchGroup{Name="KoloNaukowePierwsze",CreatedAt=DateTime.UtcNow,Department = "Elektroniki",
                OwnerId = leaderID, Leader = "Jan Kowalski", Attendant = "Adam Nowak", Description = "Opis koła naukowego #1", Subjects = new List<Subject>{ new Subject {Name = "C#" }, new Subject { Name = "Programowanie" } } },
            new StudentResearchGroup{Name="KoloNaukoweDrugie",CreatedAt=DateTime.UtcNow,Department = "Mechaniczny",
                OwnerId = leaderID, Leader = "Adam Nowak", Attendant = "Jan Kowalski", Description = "Opis koła naukowego #2", Subjects = new List<Subject>{ new Subject {Name = "Pojazdy mobilne" }, new Subject { Name = "Mechanika" } } },
            new StudentResearchGroup{Name="KoloNaukoweTrzecie",CreatedAt=DateTime.UtcNow,Department = "Chemiczny",
                OwnerId = leaderID, Leader = "Adam Piątek", Attendant = "Jan Nowak", Description = "Opis koła naukowego #3", Subjects = new List<Subject>{ new Subject {Name = "Tworzywa sztuczne" }, new Subject { Name = "Chemia" } } },
            new StudentResearchGroup{Name="KoloNaukoweCzwarte",CreatedAt=DateTime.UtcNow,Department = "Architektury",
                OwnerId = leaderID, Leader = "Adam Kowalski", Attendant = "Adam Piątek", Description = "Opis koła naukowego #4", Subjects = new List<Subject>{ new Subject {Name = "Mosty" }, new Subject { Name = "Hotele" } } },
            new StudentResearchGroup{Name="KoloNaukowePiąte",CreatedAt=DateTime.UtcNow,Department = "Energetyczny",
                OwnerId = leaderID, Leader = "Witold Nowak", Attendant = "Adam Kowalski", Description = "Opis koła naukowego #5", Subjects = new List<Subject>{ new Subject {Name = "Energie Odnawialne" }, new Subject { Name = "Energetyka jądrowa" } } },
            };

            foreach (StudentResearchGroup s in studentGroups)
            {
                context.StudentResearchGroups.Add(s);
            }
            context.SaveChanges();

        }
    }
}