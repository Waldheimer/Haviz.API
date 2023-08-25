using System.Collections.Generic;

namespace HAViz.API.Models
{
    #nullable enable
    public class YamlDefinition
    {
        public string? id { get; set; }
        public string? alias { get; set; }
        public string? description { get; set; }
        public List<Trigger>? trigger { get; set; }
        public List<Condition>? condition { get; set; }
        public List<Action>? action { get; set; }
        public string? mode { get; set; }
    }
}
