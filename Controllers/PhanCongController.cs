using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhathan_asp.Data;
using nhathan_asp.Models;

namespace nhathan_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhanCongController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET ALL: Lấy danh sách đầy đủ thông tin chi tiết
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.PhanCongs
                .Include(p => p.GiangVien)
                .Include(p => p.MonHoc)
                .Include(p => p.Lop)
                .ToListAsync();
            return Ok(data);
        }

        // 2. GET BY ID: Lấy chi tiết một bản ghi
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pc = await _context.PhanCongs
                .Include(p => p.GiangVien)
                .Include(p => p.MonHoc)
                .Include(p => p.Lop)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (pc == null) return NotFound("Không tìm thấy bản ghi phân công.");
            return Ok(pc);
        }

        // 3. POST: Thêm mới (Chấp nhận JSON đầy đủ lồng nhau mà không bị lỗi)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhanCong model)
        {
            if (model == null) return BadRequest("Dữ liệu gửi lên không hợp lệ.");

            try
            {
                // Bước quan trọng: Chỉ tạo bản ghi mới dựa trên các ID khóa ngoại.
                // Việc này tránh lỗi Entity Framework cố gắng chèn đè vào các bảng GiangVien/Lop đã có sẵn.
                var pc = new PhanCong
                {
                    GiangVienId = model.GiangVienId,
                    MonHocId = model.MonHocId,
                    LopId = model.LopId,
                    HocKy = model.HocKy
                };

                _context.PhanCongs.Add(pc);
                await _context.SaveChangesAsync();

                // Sau khi lưu, chúng ta truy vấn lại chính bản ghi đó kèm Include 
                // để kết quả trả về (Response) hiển thị đầy đủ thông tin y hệt mẫu của thầy.
                var result = await _context.PhanCongs
                    .Include(p => p.GiangVien)
                    .Include(p => p.MonHoc)
                    .Include(p => p.Lop)
                    .FirstOrDefaultAsync(x => x.Id == pc.Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Trả về lỗi chi tiết từ hệ thống nếu có sự cố Database
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Lỗi hệ thống: {errorMessage}");
            }
        }

        // 4. PUT: Cập nhật thông tin
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PhanCong model)
        {
            if (id != model.Id) return BadRequest("ID không khớp.");

            var pc = await _context.PhanCongs.FindAsync(id);
            if (pc == null) return NotFound("Không tìm thấy dữ liệu để cập nhật.");

            pc.GiangVienId = model.GiangVienId;
            pc.MonHocId = model.MonHocId;
            pc.LopId = model.LopId;
            pc.HocKy = model.HocKy;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!", data = pc });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi cập nhật: " + ex.Message);
            }
        }

        // 5. DELETE: Xóa bản ghi
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pc = await _context.PhanCongs.FindAsync(id);
            if (pc == null) return NotFound("Không tìm thấy bản ghi.");

            _context.PhanCongs.Remove(pc);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã xóa phân công thành công." });
        }
    }
}