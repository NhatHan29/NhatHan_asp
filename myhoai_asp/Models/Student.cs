using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace myhoai_asp.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string MaSV { get; set; } = string.Empty;
        public string TenSV { get; set; } = string.Empty;

        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; } = "Nam";

        public int LopId { get; set; }

        // 👇 BẮT BUỘC phải có
        [ForeignKey("LopId")]
        public Lop? Lop { get; set; }

        public ICollection<Diem>? Diems { get; set; }
    }
}