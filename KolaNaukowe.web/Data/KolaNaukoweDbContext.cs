using KolaNaukowe.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Data
{
    public class KolaNaukoweDbContext : IdentityDbContext<ApplicationUser>
    {
        public KolaNaukoweDbContext(DbContextOptions<KolaNaukoweDbContext> options):base(options) 
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<StudentResearchGroup> StudentResearchGroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
