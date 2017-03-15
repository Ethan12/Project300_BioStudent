using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    [Table("Modules")]
    public class Modules
    {
        [Key]
        public int Id { get; set; }
        public string ModuleName { get; set; }
        [ForeignKey("Lecturers")]
        public string LecturerId { get; set; }

        public virtual ApplicationUser Lecturers { get; set; }
    }
}