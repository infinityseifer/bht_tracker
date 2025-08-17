using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleSheetsProxyController : ControllerBase
    {
        private readonly HttpClient _http;

        public GoogleSheetsProxyController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        [HttpPost("drform")]
        public async Task<IActionResult> PostDrForm([FromBody] Dictionary<string, object> payload)
        {
            // Forward as form-encoded to Apps Script (no CORS between servers)
            var form = new FormUrlEncodedContent(payload
                .ToDictionary(kv => kv.Key, kv => kv.Value?.ToString() ?? ""));

            var resp = await _http.PostAsync(
                "https://script.google.com/macros/s/AKfycbxguMWev7fWLYEUO0l_hMwEPTksI1au4WH7Fw0dRO6QT-8MOUDiY9S5sxx8vOwRh0Zm/exec", form);

            if (!resp.IsSuccessStatusCode)
                return StatusCode((int)resp.StatusCode, await resp.Content.ReadAsStringAsync());

            return Ok(new { message = "Submitted to Google Sheets" });
        }
    }
}
