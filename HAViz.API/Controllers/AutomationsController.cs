using HAViz.API.Models;
using HAViz.API.Services;
using HAViz.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HAViz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationsController : ControllerBase
    {
        IHA_DataService _service;
        public AutomationsController(IHA_DataService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<string>?> GetAll()
        {
            return await _service.GetAllAutomationsAsync();
        }
        [HttpGet("states")]
        public async Task<IEnumerable<AutomationStateEntry>?> GetStates()
        {
            return await _service.GetAutomationStatesAsync();
        }
        [HttpGet("yaml/{name}")]
        public async Task<YamlDefinition?> GetYamlById([FromRoute] string name)
        {
            await Console.Out.WriteLineAsync($"Requesting : {name}");
            string id = await _service.GetAutomationIdByName(name);
              
            await Console.Out.WriteLineAsync(id);
            return await _service.GetAutomationYamlAsync(id);
        }
    }
}
