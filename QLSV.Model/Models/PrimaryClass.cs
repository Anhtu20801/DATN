using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class PrimaryClass
    {
        [Key]
        public int PrimaryClassId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public int MajorId { get; set; }
        [ValidateNever]
        public virtual Major Major { get; set; }
        
        [ValidateNever]
        public virtual ICollection<Student> Students { get; set; }
    }
}