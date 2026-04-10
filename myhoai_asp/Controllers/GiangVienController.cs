using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myhoai_asp.Data;
using myhoai_asp.Models;

namespace myhoai_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GiangVienController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.GiangViens.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiangVien model)
        {
            try
            {
                // Khớp đúng các trường trong file GiangVien.cs bạn gửi
                var newGV = new GiangVien
                {
                    TenGV = model.TenGV,
                    Email = model.Email,
                    SoDienThoai = model.SoDienThoai
                };

                _context.GiangViens.Add(newGV);
                await _context.SaveChangesAsync();

                return Ok(newGV);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Lỗi: {msg}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gv = await _context.GiangViens.FindAsync(id);
            if (gv == null) return NotFound();

            _context.GiangViens.Remove(gv);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công!" });
        }
    }
}