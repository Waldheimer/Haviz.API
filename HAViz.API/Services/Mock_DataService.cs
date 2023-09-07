
using HAViz.API.Models;
using HAViz.Shared.Models;
using Newtonsoft.Json;

namespace HAViz.API.Services
{
    public class Mock_DataService : IHA_DataService
    {
        IConfiguration _configuration;
        string api_source;
        public Mock_DataService(IConfiguration configuration)
        {
            _configuration = configuration;
            api_source = _configuration.GetSection("API_Mock_URL").Get<string>();
        }
        public List<string>? GetEntityFilter()
        {
            return _configuration.GetSection("Entity_Excludes").Get<List<string>>();
        }
        public async Task<List<string>?> GetEntityIncludes()
        {
            return _configuration.GetSection("Entity_Includes").Get<List<string>>();
        }
        public async Task<IEnumerable<LogEntry>?> GetLogAsync()
        {
            string log = File.ReadAllText($"{api_source}logbook.json");
            return JsonConvert.DeserializeObject<LogEntry[]>(log);
        }
        public async Task<IEnumerable<LogEntry>> GetFilteredLogAsync()
        {
            List<string> includes = await GetEntityIncludes();
            includes.Add("automation");
            var entries = await GetLogAsync();
            foreach (var item in entries)
            {
                if (DateTime.TryParse(item.state, out DateTime result)) { item.state = "Pressed"; }
            }
            return (from include in includes
                    from data in entries
                    where data.entity_id == include
                    select data).OrderBy(e => e.when);
        }
        public async Task<IEnumerable<string>> GetAllAutomationsAsync()
        {
            var logEntries = await GetLogAsync();
            return (from data in logEntries
                    where data.entity_id.StartsWith("automation")
                    select data.entity_id).Distinct();
        }

        public async Task<IEnumerable<Entity>> GetAllEntitiesAsync()
        {
            var excludes = await GetEntityIncludes();
            string states = File.ReadAllText($"{api_source}states.json");
            return from exes in excludes
                   from data in JsonConvert.DeserializeObject<IEnumerable<Entity>>(states)
                   where data.state != "unavailable"
                   where data.entity_id.StartsWith(exes)
                   select data;
        }
        public async Task<Entity> GetEntityInfoAsync(string id)
        {
            var entities = await GetAllEntitiesAsync();
            return (from data in entities where data.entity_id == id
                   select data).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> GetAllEntityNamesAsync()
        {
            var logEntries = await GetLogAsync();
            return (from data in logEntries
                    where !data.entity_id.StartsWith("automation")
                    select data.entity_id).Distinct();
        }
        public async Task<IEnumerable<AutomationStateEntry>> GetEntityStatesAsync()
        {
            string states = File.ReadAllText($"{ api_source}states.json");
            return (from filters in GetEntityFilter()
                    from data in JsonConvert.DeserializeObject<IEnumerable<AutomationStateEntry>>(states)
                    where data.entity_id.StartsWith(filters)
                    select data).DistinctBy(e => e.attributes.friendly_name);
        }
        public async Task<IEnumerable<AutomationStateEntry>> GetAutomationStatesAsync()
        {
            string states = File.ReadAllText($"{api_source}states.json");
            return from data in JsonConvert.DeserializeObject<AutomationStateEntry[]>(states)
                   where data.entity_id.StartsWith("automation") && !data.state.Equals("unavailable")
                   select data;
        }
        public async Task<string> GetAutomationIdByName(string name)
        {
            var allstates = await GetAutomationStatesAsync();
            await Console.Out.WriteLineAsync(allstates.Count().ToString());
            return (from data in allstates where data.attributes.friendly_name.ToLower().Contains(name.ToLower()) select data.attributes.id).First();
        }
        public async Task<YamlDefinition> GetAutomationYamlAsync(string id)
        {
            string content = File.ReadAllText($"{api_source}{id}.json");
            var pre = JsonConvert.DeserializeObject<YamlDefinition>(content);
            foreach ( var item in pre.trigger)
            {
                string eid = item.entity_id.ToString();
                List<string> ids = new List<string>();
                try { ids = JsonConvert.DeserializeObject<List<string>>(eid); }
                catch { ids.Add(eid); }
                item.entity_id = ids;
            }
            return pre;
        }

        public Task<IEnumerable<Entity>?> GetFilteredEntitiesAsync(List<string> filters)
        {
            throw new NotImplementedException();
        }
    }
}
