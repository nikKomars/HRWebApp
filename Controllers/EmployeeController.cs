using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRWebApp.Data;
using HRWebApp.Models;
using System.Linq.Dynamic.Core;

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EmployeeController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? search,
            string? status,
            DateTime? hireFrom,
            DateTime? hireTo,
            string? sortBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10)
        {
            var q = _db.Employees
                       .Include(e => e.Organization)
                       .Include(e => e.Profession)
                       .Include(e => e.Category)
                       .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                q = q.Where(e =>
                    e.FirstName.Contains(search) ||
                    e.LastName.Contains(search) ||
                    e.Organization.Name.Contains(search) ||
                    e.Profession.Name.Contains(search) ||
                    e.Category.Name.Contains(search)
                );
            }

            if (!string.IsNullOrWhiteSpace(status))
                q = q.Where(e => e.Status == status);

            if (hireFrom.HasValue)
                q = q.Where(e => e.HireDate >= hireFrom.Value);

            if (hireTo.HasValue)
                q = q.Where(e => e.HireDate <= hireTo.Value);

            q = q.OrderBy($"{sortBy} {(desc ? "descending" : "ascending")}");

            var total = await q.CountAsync();
            var list = await q.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();

            return Ok(new { Total = total, Page = page, PageSize = pageSize, Data = list });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var e = await _db.Employees
                             .Include(x => x.Organization)
                             .Include(x => x.Profession)
                             .Include(x => x.Category)
                             .FirstOrDefaultAsync(x => x.Id == id);

            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            _db.Employees.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e == null) return NotFound();
            _db.Employees.Remove(e);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
