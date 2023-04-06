﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public DateTime AttenTime { get; set; } = DateTime.Now;
        public bool Check { get; set; }
        public string? Note { get; set; }

        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public virtual Result Results { get; set; }
    }
}