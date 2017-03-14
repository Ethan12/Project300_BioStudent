using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    public class StudentDbContext : DbContext
    {
        public string CourseName { get; set; }
        public DbSet<StudentUserAccount> StudentUserAccounts { get; set; }
        

        public StudentDbContext() :base("DefaultConnection")
        {

        }
        public DbSet<StudentGrades> StudentGrades { get; set; }
    }
}