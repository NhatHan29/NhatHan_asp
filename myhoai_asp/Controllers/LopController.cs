using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myhoai_asp.Data;
using myhoai_asp.Models;

namespace myhoai_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LopController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách lớp
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Chỉ lấy thông tin lớp, không lấy kèm Students để tránh nặng máy và lỗi vòng lặp
            return Ok(await _context.Lops.ToListAsync());
        }

        // 2. Lấy chi tiết 1 lớp (có kèm danh sách sinh viên trong lớp đó)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lop = await _context.Lops
                .Include(l => l.Students)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lop == null) return NotFound("Không tìm thấy lớp này.");
            return Ok(lop);
        }

        // 3. Thêm lớp mới
        [HttpPost]
        public async Task<IActionResult> Create(Lop model)
        {
            var newLop = new Lop
            {
                TenLop = model.TenLop,
                Khoa = model.Khoa
            };
            _context.Lops.Add(newLop);
            await _context.SaveChangesAsync();
            return Ok(newLop);
        }

        // 4. Cập nhật thông tin lớp
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Lop model)
        {
            if (id != model.Id) return BadRequest("ID không khớp.");

            var lop = await _context.Lops.FindAsync(id);
            if (lop == null) return NotFound();

            lop.TenLop = model.TenLop;
            lop.Khoa = model.Khoa;

            await _context.SaveChangesAsync();
            return Ok(lop);
        }

        // 5. Xóa lớp
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lop = await _context.Lops.FindAsync(id);
            if (lop == null) return NotFound();

            _context.Lops.Remove(lop);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa lớp thành công!" });
        }
    }
}