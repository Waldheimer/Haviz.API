using HAViz.API.Services;
using HAViz.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAViz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogbookController : ControllerBase
    {
        IHA_DataService _service;
        public LogbookController(IHA_DataService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<LogEntry>?> GetLog()
        {
            return await _service.GetLogAsync();
        }
        [HttpGet("filtered")]
        public async Task<IEnumerable<LogEntry>?> GetLogFiltered()
        {
            return await _service.GetFilteredLogAsync();
        }
    }
}
