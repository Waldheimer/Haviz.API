using System.Collections.Generic;

namespace HAViz.API.Models
{
    public class  Condition
    {
        public string condition { get; set; }
        public string entity_id { get; set; }
        public string state { get; set; }
        public List<Condition> conditions { get; set; }
    }
}
