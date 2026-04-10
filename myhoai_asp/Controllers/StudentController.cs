using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myhoai_asp.Data;
using myhoai_asp.Models;

namespace myhoai_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Students
                .Include(s => s.Lop)
                .Select(s => new
                {
                    s.Id,
                    s.MaSV,
                    s.TenSV,
                    s.NgaySinh,
                    s.GioiTinh,
                    s.LopId,
                    Lop = s.Lop != null ? new
                    {
                        s.Lop.Id,
                        s.Lop.TenLop,
                        s.Lop.Khoa
                    } : null
                })
                .ToListAsync();

            return Ok(data);
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var s = await _context.Students
                .Include(s => s.Lop)
                .Include(s => s.Diems)
                .ThenInclude(d => d.MonHoc)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (s == null) return NotFound();

            return Ok(new
            {
                s.Id,
                s.MaSV,
                s.TenSV,
                s.NgaySinh,
                s.GioiTinh,
                s.LopId,
                Lop = s.Lop != null ? new
                {
                    s.Lop.Id,
                    s.Lop.TenLop,
                    s.Lop.Khoa
                } : null,
                Diems = s.Diems.Select(d => new
                {
                    d.Id,
                    d.StudentId,
                    d.MonHocId,
                    d.Score,
                    MonHoc = d.MonHoc != null ? new
                    {
                        d.MonHoc.Id,
                        d.MonHoc.TenMon,
                        d.MonHoc.SoTinChi
                    } : null
                })
            });
        }

        // ✅ CREATE (NHẬN JSON FULL NHƯ THẦY YÊU CẦU)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔥 Nếu có Lop thì lấy LopId
            if (model.Lop != null)
            {
                model.LopId = model.Lop.Id;
            }

            // ❌ tránh EF insert bảng liên quan
            model.Lop = null;
            model.Diems = null;

            // ❌ không cho set Id
            model.Id = 0;

            _context.Students.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student model)
        {
            if (id != model.Id)
                return BadRequest("Id không khớp");

            var existing = await _context.Students.FindAsync(id);
            if (existing == null)
                return NotFound();

            // 🔥 nếu có Lop thì lấy LopId
            if (model.Lop != null)
            {
                existing.LopId = model.Lop.Id;
            }
            else
            {
                existing.LopId = model.LopId;
            }

            existing.MaSV = model.MaSV;
            existing.TenSV = model.TenSV;
            existing.NgaySinh = model.NgaySinh;
            existing.GioiTinh = model.GioiTinh;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _context.Students.FindAsync(id);
            if (s == null)
                return NotFound();

            _context.Students.Remove(s);
            await _context.SaveChangesAsync();

            return Ok("Xóa thành công");
        }

        // 🔥 BONUS: GPA
        [HttpGet("gpa/{id}")]
        public async Task<IActionResult> GetGPA(int id)
        {
            var diem = await _context.Diems
                .Where(d => d.StudentId == id)
                .ToListAsync();

            if (diem.Count == 0) return Ok(0);

            var avg = diem.Average(d => d.Score);
            return Ok(avg);
        }
    }
}