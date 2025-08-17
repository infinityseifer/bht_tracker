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

        // POST: api/BhtForm
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BhtForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // NOTE: If your EF model stores List<string> directly, ensure
            // you've configured value converters or JSON column types in AppDbContext.
            _context.BhtForms.Add(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "BHT referral saved", id = form.Id });
        }

        // GET: api/BhtForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BhtForm>>> GetForms()
        {
            return await _context.BhtForms.AsNoTracking().ToListAsync();
        }
    }
}
 