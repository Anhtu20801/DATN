using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class Classroom
    {
        [Key]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        [Key]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; } //Thoi gian bat dau mo hoc phan

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; } //Thoi gian ket thuc lop lop hoc phan

        public int Semester { get; set; } //Hoc ky
        public string Lesson { get; set; } //Tiet hoc
        [ValidateNever]
        public virtual Course Courses { get; set; }
        [ValidateNever]
        public virtual Teacher Teachers { get; set; }
        [ValidateNever]
        public virtual ICollection<Result> Results { get; set; }
    }
}