using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading.Tasks;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleAuthController : ControllerBase
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] string credentialFilePath)
        {
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("/app/credentials", credentialFilePath);

            // Использование небезопасного пути без валидации
            GoogleCredential credential = await GoogleCredential.FromFileAsync(fullPath);

            return Ok("Authentication successful.");
        }
    }
}