using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    [Table("StudentGrades")]
    public class StudentGrades
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public string Grade { get; set; }

        public virtual StudentUserAccount Student { get; set; }
    }
}