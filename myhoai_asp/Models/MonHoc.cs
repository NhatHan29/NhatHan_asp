using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myhoai_asp.Models
{
    [Table("Subjects")] // tên bảng trong SQL
    public class MonHoc
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên môn không được để trống")]
        [StringLength(100)]
        public string TenMon { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Số tín chỉ phải từ 1 đến 10")]
        public int SoTinChi { get; set; }

        // Quan hệ 1 - nhiều với bảng Diem
        public virtual ICollection<Diem>? Diems { get; set; }
    }
}