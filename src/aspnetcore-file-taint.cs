using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessFilePath([FromBody] string untrustedPath)
        {
            // Использование Path.Combine с небезопасными данными
            string combinedPath = Path.Combine("/base/directory", untrustedPath);

            // Использование StringBuilder для манипуляции строкой пути
            StringBuilder sb = new StringBuilder();
            sb.Append(combinedPath);

            // Использование небезопасного пути в методе ASP.NET Core
            SendFile(sb.ToString());

            return Ok("File processed");
        }

        private void SendFile(string path)
        {
            // Пример использования небезопасного пути в ASP.NET Core
            Console.WriteLine($"Sending file from: {path}");
            // Здесь может быть код для отправки файла
        }
    }
}