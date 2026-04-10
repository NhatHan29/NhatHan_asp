using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myhoai_asp.Models
{
    [Table("Assignments")] // Tên bảng trong SQL là Assignments
    public class PhanCong
    {
        [Key]
        public int Id { get; set; }

        // Khóa ngoại tới Giảng viên
        [Required(ErrorMessage = "Vui lòng chọn Giảng viên")]
        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien? GiangVien { get; set; }

        // Khóa ngoại tới Môn học
        [Required(ErrorMessage = "Vui lòng chọn Môn học")]
        public int MonHocId { get; set; }

        [ForeignKey("MonHocId")]
        public virtual MonHoc? MonHoc { get; set; }

        // Khóa ngoại tới Lớp học
        [Required(ErrorMessage = "Vui lòng chọn Lớp học")]
        public int LopId { get; set; }

        [ForeignKey("LopId")]
        public virtual Lop? Lop { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Học kỳ")]
        [StringLength(50)]
        public string HocKy { get; set; } = string.Empty;
    }
}