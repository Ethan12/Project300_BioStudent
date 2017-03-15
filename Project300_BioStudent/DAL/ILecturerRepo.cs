using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project300_BioStudent.Models;

namespace Project300_BioStudent.DAL
{
    public interface ILecturerRepo : IDisposable
    {
        ApplicationUser GetItemByid(string UserID);
    }
}