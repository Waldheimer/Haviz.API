using HAViz.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAViz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheck : ControllerBase
    {
        IHA_DataService _service;
        public HealthCheck(IHA_DataService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get() 
        { 
            return Ok("Server up and Running...");
        }
        [HttpGet("filter")]
        public List<string> getFilters()
        {
            return _service.GetEntityFilter();
        }
    }
}
