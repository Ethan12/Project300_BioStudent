﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project300_BioStudent.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string TeachingField { get; set; }
        public string Institute { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<Project300_BioStudent.Models.StudentUserAccount> StudentUserAccounts { get; set; }
        public System.Data.Entity.DbSet<Project300_BioStudent.Models.StudentGrades> StudentGrades { get; set; }
        public System.Data.Entity.DbSet<Project300_BioStudent.Models.Modules> Modules { get; set; }
        public System.Data.Entity.DbSet<Project300_BioStudent.Models.StudentAttendance> Attendance { get; set; }
        public System.Data.Entity.DbSet<Project300_BioStudent.Models.StudentEnrolment> Enrolment { get; set; }
    }
}