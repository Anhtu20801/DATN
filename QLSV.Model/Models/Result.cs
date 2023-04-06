﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class Result
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public virtual Classroom Classroom { get; set; }
        
        public virtual ICollection<Attendance> Attendances { get; set; }

        
        public int? RegularMark { get; set; } // Diem kiem tra thuong xuyen
        public int? MitermMark { get; set; } // Diem kiem tra giua ky
        public int? FinalMark { get; set; } // Diem kiem tra cuoi ky
        public string? Note { get; set; }
        
    }
}