using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhathan_asp.Models
{
    [Table("Scores")]
    public class Diem
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int MonHocId { get; set; }

        public float Score { get; set; }

        // 👇 BẮT BUỘC phải có 2 dòng này
        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }

        [ForeignKey("MonHocId")]
        public virtual MonHoc? MonHoc { get; set; }
    }
}