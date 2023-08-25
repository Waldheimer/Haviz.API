namespace HAViz.API.Models
{
    #nullable enable
    public class Action
    {
        public string? type { get; set; }
        public string? device_id { get; set; }
        public string? entity_id { get; set; }
        public string? domain { get; set; }
        public string? service { get; set; }
        public Data? data { get; set; }
        public Target? target { get; set; }
    }
}
