using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhathan_asp.Models
{
    [Table("Classes")]
    public class Lop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TenLop { get; set; } = string.Empty;

        public string Khoa { get; set; } = string.Empty;

        // Quan hệ
        public virtual ICollection<Student>? Students { get; set; }
    }
}