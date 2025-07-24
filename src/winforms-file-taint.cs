using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class MyController : Controller
    {
        [HttpPost]
        public IActionResult LoadFilePath([FromBody] string untrustedPath)
        {
            // Использование Path.Combine с небезопасными данными
            string combinedPath = Path.Combine("C:\\base\\directory", untrustedPath);

            // Использование StringBuilder для манипуляции строкой пути
            StringBuilder sb = new StringBuilder();
            sb.Append(combinedPath);

            // Использование небезопасного пути в методе WinForms
            LoadFileIntoRichTextBox(sb.ToString());

            return Ok("File loaded");
        }

        private void LoadFileIntoRichTextBox(string path)
        {
            using (RichTextBox richTextBox = new RichTextBox())
            {
                // Загрузка файла в RichTextBox с использованием небезопасного пути
                richTextBox.LoadFile(path);
                Console.WriteLine($"File loaded from: {path}");
            }
        }
    }
}