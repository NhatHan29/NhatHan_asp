using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myhoai_asp.Data;
using myhoai_asp.Models;

namespace myhoai_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiemController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: Lấy danh sách điểm (Join để hiện tên SV và tên Môn)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Diems
                .Include(d => d.Student)
                    .ThenInclude(s => s.Lop) // Lấy thông tin lớp của sinh viên
                .Include(d => d.MonHoc)
                .ToListAsync();

            return Ok(data);
        }

        // 2. POST: Thêm điểm (Hàm xử lý triệt để lỗi 500 Identity Insert)
        [HttpPost]
        public async Task<IActionResult> Create(Diem model)
        {
            try
            {
                // CÁCH FIX TRIỆT ĐỂ: 
                // Tạo một object Diem mới, chỉ lấy các giá trị số (Id) cần thiết.
                // Việc này loại bỏ hoàn toàn các object 'student' và 'monHoc' lồng nhau
                // vốn là nguyên nhân gây ra lỗi "Cannot insert explicit value for identity column".

                var newDiem = new Diem
                {
                    StudentId = model.StudentId,
                    MonHocId = model.MonHocId,
                    Score = model.Score
                };

                _context.Diems.Add(newDiem);
                await _context.SaveChangesAsync();

                // Trả về model ban đầu để Swagger hiển thị đẹp đúng mẫu thầy yêu cầu
                return Ok(model);
            }
            catch (Exception ex)
            {
                // Trả về lỗi chi tiết từ InnerException nếu có
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Lỗi Database: {message}");
            }
        }

        // 3. GET theo ID: Xem chi tiết một đầu điểm
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var diem = await _context.Diems
                .Include(d => d.Student)
                .Include(d => d.MonHoc)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diem == null) return NotFound("Không tìm thấy điểm số này.");

            return Ok(diem);
        }

        // 4. DELETE: Xóa điểm
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _context.Diems.FindAsync(id);
            if (d == null) return NotFound("Dữ liệu không tồn tại.");

            _context.Diems.Remove(d);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đã xóa điểm thành công!" });
        }
    }
}