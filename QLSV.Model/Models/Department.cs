using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Model.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        [ValidateNever]
        public virtual ICollection<PrimaryClass> PrimaryClasses { get; set; }
    }
}
