using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    [Table("StudentAttendance")]
    public class StudentAttendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public int Attended { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public virtual StudentUserAccount Student { get; set; }

        public virtual Modules Module { get; set; }
    }
}