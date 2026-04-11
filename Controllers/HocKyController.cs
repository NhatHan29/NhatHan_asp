using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhathan_asp.Data;
using nhathan_asp.Models;

namespace nhathan_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocKyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HocKyController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách học kỳ
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.HocKys.ToListAsync());
        }

        // 2. Thêm mới học kỳ (Ví dụ: HK1 - 2026)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HocKy model)
        {
            _context.HocKys.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // 3. Cập nhật học kỳ
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HocKy model)
        {
            if (id != model.Id) return BadRequest();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Cập nhật thành công");
        }

        // 4. Xóa học kỳ
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hk = await _context.HocKys.FindAsync(id);
            if (hk == null) return NotFound();

            _context.HocKys.Remove(hk);
            await _context.SaveChangesAsync();
            return Ok("Đã xóa học kỳ");
        }
    }
}