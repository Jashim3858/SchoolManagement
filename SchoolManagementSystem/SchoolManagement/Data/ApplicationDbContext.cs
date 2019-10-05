using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.AccountViewModels;



namespace SchoolManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<SchoolManagement.Models.AccountViewModels.RegisterViewModel> RegisterViewModel { get; set; }

        public DbSet<SchoolManagement.Models.ClassInformation> ClassInformation { get; set; }

        public DbSet<SchoolManagement.Models.StudentInformation> StudentInformation { get; set; }
        public DbSet<SchoolManagement.Models.TeacherInformation> TeacherInformation_1{ get; set; }
    }
}
