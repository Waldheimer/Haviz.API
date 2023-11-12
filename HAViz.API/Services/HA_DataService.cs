using HAViz.API.Models;
using HAViz.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HAViz.API.Services
{
    public class HA_DataService : IHA_DataService
    {
        #region Fields, Properties & Constructors
        //  Fields for HttpClient, IConfiguration, the Url to Homeassistant-API and the 
        //  Bearer-Token to Access the API
        HttpClient _httpClient;
        IConfiguration _configuration;
        string? _url, _token;
        //  -----------------------------------------------------------------------------
        //  Constructor with DependencyInjection of IConfiguration and HttpClient
        public HA_DataService(IConfiguration configuration, HttpClient client)
        {
            _httpClient = client;
            _configuration = configuration;
            _url = _configuration.GetSection("API_Url").Value;
            _token = _configuration.GetSection("API_Token").Value;
        }
        //  -----------------------------------------------------------------------------
        #endregion

        #region Filter
        //  Get the Filter for the wanted Entities from appsettings.json
        public List<string>? GetEntityFilter()
        {
            return _configuration.GetSection("Entity_Includes").Get<List<string>>();
        }
        //  -----------------------------------------------------------------------------

        #endregion

        #region Logbook
        //  Read the complete or the filtered Logbook from Homeassistant
        public async Task<IEnumerable<LogEntry>?> GetLogAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/logbook"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<LogEntry>>(res);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new LogEntry[] { };
                }
            }
        }
        public async Task<IEnumerable<LogEntry>?> GetFilteredLogAsync()
        {
            var filters = GetEntityFilter();
            var logs = await GetLogAsync();
            return from filter in filters
                   from data in logs!
                   where data.entity_id.Contains(filter)
                   select data;
        }
        //  -----------------------------------------------------------------------------
        #endregion

        #region Automations
        /// <summary>
        ///  Get All Names of the Automations available in Homeassistant
        /// </summary>
        public async Task<IEnumerable<string>?> GetAllAutomationsAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/logbook"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    return (from data in JsonConvert.DeserializeObject<LogEntry[]>(res)
                            where data.entity_id.StartsWith("automation")
                            select data.entity_id).Distinct();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<string>();
                }
            }
        }
        ///  Get the ID of an Automation by its Entity_Id
        public async Task<string?> GetAutomationIdByName(string name)
        {
            var states = await GetAutomationStatesAsync();
            return (from data in states where data.entity_id.EndsWith(name) select data.attributes.id).First();
        }
        //  Get the States of all Automations available in Homeassistant
        public async Task<IEnumerable<AutomationStateEntry>?> GetAutomationStatesAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/states"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var states = JsonConvert.DeserializeObject<AutomationStateEntry[]>(res);
                    return from data in states where data.entity_id.StartsWith("automation") select data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AutomationStateEntry[] { };
                }
            }
        }
        //  Get the YAML-Definition of an Automations by ID
        public async Task<YamlDefinition?> GetAutomationYamlAsync(string id)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/config/automation/config/{id}"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var pre = JsonConvert.DeserializeObject<YamlDefinition>(res);
                    foreach (var item in pre.trigger)
                    {
                        string eid = item.entity_id.ToString();
                        List<string> ids = new List<string>();
                        try { ids = JsonConvert.DeserializeObject<List<string>>(eid); }
                        catch { ids.Add(eid); }
                        item.entity_id = ids;
                    }
                    return pre;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        //  -----------------------------------------------------------------------------
        #endregion

        #region Entities
        public async Task<IEnumerable<Entity>?> GetAllEntitiesAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/states"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Entity[]>(res);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public async Task<IEnumerable<Entity>?> GetFilteredEntitiesAsync(List<string> filters)
        {
            var nameFilters = GetEntityFilter();
            var states = await GetAllEntitiesAsync();
            return from filter in nameFilters
                   from state in states
                   where !state.entity_id.Contains(filter)
                   select new Entity() {  };
        }

        public async Task<IEnumerable<string>?> GetAllEntityNamesAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/logbook"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();

                    var enl = (from data in JsonConvert.DeserializeObject<LogEntry[]>(res)
                               where !data.entity_id.StartsWith("automation")
                               select data.entity_id).Distinct();
                    return enl;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public async Task<Entity?> GetEntityInfoAsync(string id)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/states/{id}"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Entity>(res);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public async Task<IEnumerable<AutomationStateEntry>?> GetEntityStatesAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/states"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var states = JsonConvert.DeserializeObject<AutomationStateEntry[]>(res);
                    return from data in states where !data.entity_id.StartsWith("automation") select data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AutomationStateEntry[] { };
                }
            }
        }
        #endregion
    }
}
