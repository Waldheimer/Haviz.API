using HAViz.API.Models;
using HAViz.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public string Check() 
        { 
            return JsonConvert.SerializeObject("Server running...", Formatting.Indented);
        }
        [HttpGet("filter")]
        public List<string> getFilters()
        {
            return _service.GetEntityFilter();
        }

        [HttpGet("check/{id}")]
        public async Task<IEnumerable<YamlDefinition>> getDependencies([FromRoute] string id)
        {
            var entities = from data in await _service.GetAllEntitiesAsync() select data.entity_id;
            var automations = from data in await _service.GetAllAutomationsAsync() select data.Split(".")[1];

            

            List<YamlDefinition> definitions = new ();
            foreach (var atm in automations)
            {
                
                string atmid = await _service.GetAutomationIdByName(atm);

                //definitions.Add(atmid);
                definitions.Add(await _service.GetAutomationYamlAsync(atmid));
            }

            return definitions;
        }
    }
}
