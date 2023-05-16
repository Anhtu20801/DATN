using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Model.Models
{
    public class Major
    {
        [Key]
        public int MajorId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        
        public int DepartmentId { get; set; }
        [ValidateNever]
        public virtual Department Department { get; set; }
        [ValidateNever]
        public virtual ICollection<PrimaryClass> PrimaryClasses { get; set; }
    }
}
