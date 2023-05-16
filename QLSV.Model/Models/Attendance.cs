using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public DateTime AttenTime { get; set; }
        public bool Check { get; set; }
        public string? Note { get; set; }
        
        public int ClassroomId { get; set; }
        public int StudentId { get; set; }
        [ValidateNever]
        public virtual Result Results { get; set; }
    }
}