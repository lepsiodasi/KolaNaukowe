﻿using KolaNaukowe.web.Authorization;
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
                //konto admina(wszystkie akcje dozwolone)
                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@test.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorRole);
                //konto prezesa(edycja dozwolona)(obecnie jeden dla wszystkich kół)
                var leaderID = await EnsureUser(serviceProvider, testUserPw, "leader@test.com");
                await EnsureRole(serviceProvider, leaderID, Constants.LeaderRole);
                //konto uzytkownika(dostęp do opisu)
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

            //testowe dane
            var students = new Student[]
            {
            new Student{Name="Maciek" },
            new Student{Name="Antek" },
            };

            var students2 = new Student[]
           {
            new Student{Name="Kamil" },
            new Student{Name="Marta" },
           };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            foreach (Student s in students2)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var studentGroups = new StudentResearchGroup[]
            {
            new StudentResearchGroup{Name="EKA.NET",CreatedAt=DateTime.UtcNow,Department = "Elektroniki", OwnerId = leaderID, Students = students},
            new StudentResearchGroup{Name="PIAST.NET",CreatedAt=DateTime.UtcNow,Department = "Informatyki i Zarzadzania", OwnerId = leaderID, Students = students2},
            new StudentResearchGroup{Name="NEW.NET",CreatedAt=DateTime.UtcNow, Department = "Mechaniczny",OwnerId = leaderID},
            new StudentResearchGroup{Name="COS.NET",CreatedAt=DateTime.UtcNow, Department = "Elektryczny", OwnerId = leaderID},
            };

            foreach (StudentResearchGroup s in studentGroups)
            {
                context.StudentResearchGroups.Add(s);
            }
            context.SaveChanges();

        }
    }
}