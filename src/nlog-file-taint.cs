using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace MyApp.Controllers
{
    public class MyController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        public IActionResult LogFilePath([FromBody] string untrustedPath)
        {
            // Использование Path.Combine с небезопасными данными
            string combinedPath = Path.Combine("/base/directory", untrustedPath);

            // Использование StringBuilder для манипуляции строкой пути
            StringBuilder sb = new StringBuilder();
            sb.Append(combinedPath);

            // Использование небезопасного пути в методе NLog
            SimpleConfigurator.ConfigureForFileLogging(sb.ToString());

            return Ok("Path logged");
        }
    }
}