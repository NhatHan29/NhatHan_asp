using nhathan_asp.Data;
using nhathan_asp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhathan_asp.Data;
using nhathan_asp.Models;

namespace nhathan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MonHocController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MonHoc
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.MonHocs.ToListAsync();
            return Ok(data);
        }

        // GET: api/MonHoc/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mh = await _context.MonHocs.FindAsync(id);
            if (mh == null) return NotFound();

            return Ok(mh);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(MonHoc model)
        {
            _context.MonHocs.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MonHoc model)
        {
            if (id != model.Id) return BadRequest();

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mh = await _context.MonHocs.FindAsync(id);
            if (mh == null) return NotFound();

            _context.MonHocs.Remove(mh);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}