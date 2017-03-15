using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    public class ModuleDbContext : DbContext
    {
        public string ModuleName { get; set; }
        public DbSet<Modules> Modules { get; set; }


        public ModuleDbContext() : base("DefaultConnection")
        {

        }
    }
}