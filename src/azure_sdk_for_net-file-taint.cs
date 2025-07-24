using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace MyApp.Controllers
{
    public class MyController : Controller
    {
        [HttpPost]
        public IActionResult ProcessFilePath([FromBody] string untrustedPath)
        {
            // Использование Path.Combine с небезопасными данными
            string combinedPath = Path.Combine("/base/directory", untrustedPath);

            // Использование StringBuilder для манипуляции строкой пути
            StringBuilder sb = new StringBuilder();
            sb.Append(combinedPath);

            // Использование небезопасного пути в методе Azure SDK
            ProcessAzureFile(sb.ToString());

            return Ok("Path processed");
        }

        private void ProcessAzureFile(string path)
        {
            // Пример использования небезопасного пути в Azure SDK
            var resourceIdentifier = ResourceFile.FromUrl("https://example.com", path);
            Console.WriteLine($"Resource identifier created for path: {path}");
        }
    }
}