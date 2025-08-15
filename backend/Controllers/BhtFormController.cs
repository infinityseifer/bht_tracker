using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BhtFormController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BhtFormController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BhtForm form)
        {
            _context.BhtForms.Add(form);  // ✅ Use plural
            await _context.SaveChangesAsync();
            return Ok(new { message = "Discipline Referral Form saved", id = form.Id });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BhtForm>>> GetForms()
        {
            return await _context.BhtForms.ToListAsync();  // ✅ Use plural
        }
    }
}
