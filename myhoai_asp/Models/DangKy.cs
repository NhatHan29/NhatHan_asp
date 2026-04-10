using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myhoai_asp.Models
{
    [Table("Enrollments")]
    public class DangKy
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int HocKyId { get; set; }

        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [ForeignKey("HocKyId")]
        public HocKy? HocKy { get; set; }
    }
}