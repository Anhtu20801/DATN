using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Model.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(250)]
        public string TeacherCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public int DepartmentId { get; set; }
        [ValidateNever]
        public virtual Department Department { get; set; }
        [ValidateNever]
        public virtual ICollection<Classroom> Classrooms { get; set; }
    }
}