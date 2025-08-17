using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdaController : ControllerBase
    {
        [HttpPost]
        [RequestSizeLimit(25_000_000)] // ~25MB max upload; tune as needed
        public async Task<IActionResult> AnalyzeCsv([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only CSV files are supported.");

            var tmpDir = Path.Combine(Path.GetTempPath(), "eda");
            Directory.CreateDirectory(tmpDir);

            var csvPath = Path.Combine(tmpDir, $"{Guid.NewGuid()}.csv");
            await using (var fs = System.IO.File.Create(csvPath))
                await file.CopyToAsync(fs);

            // Call Python script
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"scripts/eda_report.py --in \"{csvPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            using var proc = Process.Start(psi);
            if (proc == null) return StatusCode(500, "Failed to start analysis process.");
            string stdout = await proc.StandardOutput.ReadToEndAsync();
            string stderr = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            System.IO.File.Delete(csvPath);

            if (proc.ExitCode != 0)
            {
                return StatusCode(500, new { error = "EDA script failed", details = stderr });
            }

            try
            {
                var jsonDoc = JsonDocument.Parse(stdout);
                return Ok(jsonDoc.RootElement);
            }
            catch
            {
                return StatusCode(500, new { error = "Invalid JSON from EDA script", details = stdout });
            }
        }
    }
}
