using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMongoDatabase _database;

        public FileController(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("mydatabase");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery] string fileName)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/uploads", fileName);

            var gridFS = new GridFSBucket(_database);

            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                // Использование небезопасного пути без валидации
                await gridFS.UploadFromStreamAsync(fileName, stream);
            }

            return Ok("File uploaded successfully.");
        }
    }
}