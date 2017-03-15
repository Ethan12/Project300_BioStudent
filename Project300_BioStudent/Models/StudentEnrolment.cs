using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    [Table("StudentEnrolment")]
    public class StudentEnrolment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Modules Module { get; set; }
        public virtual StudentUserAccount Student { get; set; }
    }
}