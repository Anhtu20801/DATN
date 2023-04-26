using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Model.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public double TheoryCreditsNumber { get; set; } //So tin chi ly thuyet

        [Required]
        public double PracticeCreditsNumber { get; set; } //So tin chi thuc hanh

        public string? Description { get; set; } // Mieu ta
        public string? Content { get; set; } //Noi dung
        public string? ExamForm { get; set; } //Hinh thuc kiem tra
        public string? Note { get; set; } //Ghi chu
        public string? SpecialRequirements { get; set; } //Yeu cau dac biet
        public string? Documentation { get; set; } //Tai lieu tham khao
        [ValidateNever]
        public virtual ICollection<Classroom> Classrooms { get; set; }
    }
}