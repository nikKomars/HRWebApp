using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Data;
using HRWebApp.Models;

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProfessionController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _db.Professions.AsQueryable();
            var total = await query.CountAsync();
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { Total = total, Page = page, PageSize = pageSize, Data = data });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Profession model)
        {
            model.Id = 0; 
            _db.Professions.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = model.Id }, model);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Profession model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Professions.FindAsync(id);
            if (item == null) return NotFound();
            _db.Professions.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
