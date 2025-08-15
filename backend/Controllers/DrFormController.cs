using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrFormController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrFormController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(DrForm form)
        {
            _context.DrForms.Add(form);  // ✅ Use plural
            await _context.SaveChangesAsync();
            return Ok(new { message = "Discipline Referral Form saved", id = form.Id });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrForm>>> GetForms()
        {
            return await _context.DrForms.ToListAsync();  // ✅ Use plural
        }
    }
}
