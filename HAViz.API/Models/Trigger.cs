using Newtonsoft.Json;
using System.Collections.Generic;

namespace HAViz.API.Models
{
    #nullable enable
    public class Trigger
    {
        public string? platform { get; set; }
        public object? entity_id { get; set; }
        public For? time { get; set; }
        public string? to { get; set; }
    }
}
