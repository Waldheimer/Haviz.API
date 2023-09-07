using HAViz.API.Services;
using HAViz.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAViz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        IHA_DataService _service;
        public EntitiesController(IHA_DataService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<Entity>?> GetAll()
        {
            return await _service.GetAllEntitiesAsync();
        }
        [HttpGet("filtered")]
        public async Task<IEnumerable<Entity>?> GetFiltered()
        {
            var filters = _service.GetEntityFilter();
            return await _service.GetFilteredEntitiesAsync(filters!);
        }
        [HttpGet("list")]
        public async Task<IEnumerable<string>?> GetNames()
        {
            return await _service.GetAllEntityNamesAsync();
        }
        [HttpGet("states")]
        public async Task<IEnumerable<AutomationStateEntry>?> GetStates()
        {
            DateTime? date = null;
            IEnumerable<AutomationStateEntry?> states = await _service.GetEntityStatesAsync();
            foreach (AutomationStateEntry item in states)
            {
                if (DateTime.TryParse(item.state, out DateTime result))
                {
                    date = result;
                    item.state = "Pressed";
                }
            }
            return states;
        }
        [HttpGet("{id}")]
        public async Task<Entity?> GetById([FromRoute] string id)
        {
            return await _service.GetEntityInfoAsync(id);
        }
    }
}
