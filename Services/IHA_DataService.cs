using HAViz.API.Models;
using HAViz.Shared.Models;

namespace HAViz.API.Services
{
    public interface IHA_DataService
    {
        List<string>? GetEntityFilter();
        Task<IEnumerable<LogEntry>?> GetLogAsync();
        Task<IEnumerable<LogEntry>?> GetFilteredLogAsync();
        Task<IEnumerable<string>?> GetAllEntityNamesAsync();
        Task<IEnumerable<Entity>?> GetAllEntitiesAsync();
        Task<IEnumerable<Entity>?> GetFilteredEntitiesAsync(List<string> filters);
        Task<IEnumerable<AutomationStateEntry>?> GetEntityStatesAsync();
        Task<Entity?> GetEntityInfoAsync(string id);
        Task<IEnumerable<AutomationStateEntry>?> GetAutomationStatesAsync();
        Task<IEnumerable<string>?> GetAllAutomationsAsync();
        Task<string?> GetAutomationIdByName(string name);
        Task<YamlDefinition?> GetAutomationYamlAsync(string id);
    }
}
