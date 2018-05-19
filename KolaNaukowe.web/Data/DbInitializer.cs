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
            new StudentResearchGroup{Name="EKA.NET",CreatedAt=DateTime.UtcNow,Department = "Elektroniki",
                OwnerId = leaderID, Leader = "Adam Pierwszy", Attendant = "Jacek Małolepszy", Description = "Koło naukowe programistów", Subjects = new List<Subject>{ new Subject {Name = "C#" }, new Subject { Name = "Programowanie" } } },
            new StudentResearchGroup{Name="PIAST.NET",CreatedAt=DateTime.UtcNow,Department = "Informatyki i Zarzadzania",
                OwnerId = leaderID, Leader = "Kamil Drugi", Attendant = "Fryderyk Kowalski", Description = "Koło naukowe programistów", Subjects = new List<Subject>{ new Subject {Name = "C#" }, new Subject { Name = ".NET" }}},
            new StudentResearchGroup{Name="NEW.NET",CreatedAt=DateTime.UtcNow, Department = "Mechaniczny",
                OwnerId = leaderID, Leader = "Adam Trzeci", Attendant = "Jan Nowak", Description = "Koło naukowe mechaników", Subjects = new List<Subject>{ new Subject {Name = "Projektowanie maszyn" } }},
            new StudentResearchGroup{Name="COS.NET",CreatedAt=DateTime.UtcNow, Department = "Elektryczny",
                OwnerId = leaderID, Leader = "Michał Czwarty", Attendant = "Pedro", Description = "Koło naukowe elektryków", Subjects = new List<Subject>{new Subject {Name = "Elektronika" } }},
            };

            foreach (StudentResearchGroup s in studentGroups)
            {
                context.StudentResearchGroups.Add(s);
            }
            context.SaveChanges();

        }
    }
}