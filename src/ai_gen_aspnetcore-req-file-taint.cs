using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpGet("read")]
        public IActionResult ReadFile([FromQuery] string filePath)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/data", filePath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            string fileContent = System.IO.File.ReadAllText(fullPath);
            return Ok(fileContent);
        }
    }
}