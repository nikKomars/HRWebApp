using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Data;
using HRWebApp.Models;

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Categories.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Categories.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            model.Id = 0;
            _db.Categories.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Categories.FindAsync(id);
            if (item == null) return NotFound();
            _db.Categories.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
