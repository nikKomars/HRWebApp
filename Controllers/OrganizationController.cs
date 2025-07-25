using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Data;
using HRWebApp.Models;

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrganizationController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Organizations.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var org = await _db.Organizations.FindAsync(id);
            if (org == null) return NotFound();
            return Ok(org);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization model)
        {
            _db.Organizations.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Organization model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var org = await _db.Organizations.FindAsync(id);
            if (org == null) return NotFound();
            _db.Organizations.Remove(org);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
