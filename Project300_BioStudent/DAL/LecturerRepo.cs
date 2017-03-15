using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project300_BioStudent.Models;

namespace Project300_BioStudent.DAL
{
    public class LecturerRepo : ILecturerRepo, IDisposable
    {
        ApplicationDbContext context;
        public LecturerRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetItemByid(string UserID)
        {
            // return this.context.Lecturers.Find(UserID);
            return this.context.Users.Find(UserID);
        }
    }
}