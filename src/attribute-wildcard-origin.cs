using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ExampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    // ruleid: attribute-wildcard-origin
    public class InsecureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "This endpoint is accessible from any origin due to wildcard CORS configuration" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class InsecureController2 : ControllerBase
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // ruleid: attribute-wildcard-origin
        public IActionResult Get()
        {
            return Ok(new { message = "This endpoint is accessible from any origin due to wildcard CORS configuration" });
        }
    }

    public class SecureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "This endpoint is only accessible from the specified origin" });
        }
    }

    [EnableCors(origins: "https://example.com,https://example2.com", headers: "*", methods: "*")]
    public class SecureController2 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
        }
    }
}
