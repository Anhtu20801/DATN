using System.ComponentModel.DataAnnotations;

namespace QLSV.Model.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } //Ho ten

        [Required]
        [MaxLength(250)]
        public string Major { get; set; } //Nganh hoc

        public string? Gender { get; set; } //Gioi tinh

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } //Ngay sinh

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAdmission { get; set; } //Ngay nhap hoc

        public string? Address { get; set; } //Dia chi

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [MaxLength(12)]
        public string CCCDNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CCCDDateStart { get; set; }//Ngay cap CCCD

        public byte[] FrontCCCDImage { get; set; } //Anh mat truoc CCCD
        public byte[] BackCCCDImage { get; set; } //Anh mat sau CCCD
        public byte[] Avatar { get; set; }
        public string? MedicareCardNumber { get; set; } //Ma the BH y te

        public int PrimaryClassId { get; set; }
        public virtual PrimaryClass PrimaryClass { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}