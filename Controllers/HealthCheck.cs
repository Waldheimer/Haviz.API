using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAViz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheck : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        { 
            return Ok("Server up and Running...");
        }
    }
}
