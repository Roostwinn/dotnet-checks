using Microsoft.AspNetCore.Mvc;
using NLog;
using System.IO;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        [HttpPost("configure")]
        public IActionResult ConfigureLogging([FromQuery] string logFilePath)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/logs", logFilePath);

            // Использование небезопасного пути без валидации
            SimpleConfigurator.ConfigureForFileLogging(fullPath);

            return Ok("Logging configured successfully.");
        }
    }
}