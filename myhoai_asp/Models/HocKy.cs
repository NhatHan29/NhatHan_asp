using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myhoai_asp.Models
{
    [Table("Semesters")]
    public class HocKy
    {
        [Key]
        public int Id { get; set; }

        public string TenHocKy { get; set; } = "HK1";

        public int NamHoc { get; set; }

        public virtual ICollection<DangKy>? DangKys { get; set; }
    }
}