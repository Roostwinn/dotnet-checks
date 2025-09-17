using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.IO;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LargeObjectController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public LargeObjectController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpPost("import")]
        public IActionResult ImportLargeObject([FromQuery] string filePath)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/uploads", filePath);

            using (var loManager = new NpgsqlLargeObjectManager(_connection))
            {
                // Использование небезопасного пути без валидации
                loManager.ImportRemote(fullPath);
            }

            return Ok("File imported successfully.");
        }
    }
}