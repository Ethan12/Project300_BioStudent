using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    [Table("StudentUserAccount")]
    public class StudentUserAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is Required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Course Name is Required.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Student Number is Required.")]
        [RegularExpression(@"^[a-zA-Z]\d{8}", ErrorMessage = "Incorrect format! Format must be s/S00123456")]
        public string StudentNum { get; set; }

        [Required(ErrorMessage = "Student Email is Required")]
        [RegularExpression(@"^[a-zA-Z]\d{8}@[a-zA-Z]{4}(.[a-zA-Z]{7})(.[a-zA-Z]{2})")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Password don't match!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int FingerprintID { get; set; }
    }
}