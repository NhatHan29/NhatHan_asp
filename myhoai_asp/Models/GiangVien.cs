using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myhoai_asp.Models
{
    [Table("Teachers")]
    public class GiangVien
    {
        [Key]
        public int Id { get; set; }

        public string TenGV { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SoDienThoai { get; set; } = string.Empty;
    }
}