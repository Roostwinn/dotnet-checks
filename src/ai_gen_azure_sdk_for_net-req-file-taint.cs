using Microsoft.AspNetCore.Mvc;
using Azure;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob([FromQuery] string filePath)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/uploads", filePath);

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("my-container");
            BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(fullPath));

            using (FileStream uploadFileStream = System.IO.File.OpenRead(fullPath))
            {
                // Использование небезопасного пути без валидации
                await blobClient.UploadAsync(uploadFileStream, true);
            }

            return Ok("File uploaded successfully.");
        }
    }
}