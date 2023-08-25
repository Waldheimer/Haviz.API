namespace HAViz.Shared.Models
{
    [Serializable]
    public class LogEntry
    {
        private DateTime? _when = null;
        private string _state = string.Empty;
        private string _entity_id = string.Empty;
        private string _name = string.Empty;
        private string _domain = string.Empty;
        private string _context_domain = string.Empty;
        private string _context_event_type = string.Empty;
        private string _source = string.Empty;
        private string _context_state = string.Empty;
        private string _context_entity_id = string.Empty;
        private string _context_source = string.Empty;

        public DateTime? when { get { return _when; } set { _when = value; } }
        public string state { get { return _state; } set { _state = value; } }
        public string entity_id { get { return _entity_id; } set { _entity_id = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string domain { get { return _domain; } set { _domain = value; } }
        public string context_domain { get { return _context_domain; } set { _context_domain = value; } }
        public string context_event_type { get { return _context_event_type; } set { _context_event_type = value; } }
        public string source { get { return _source; } set { _source = value; } }
        public string context_state { get { return _context_state; } set { _context_state = value; } }
        public string context_entity_id { get { return _context_entity_id; } set { _context_entity_id = value; } }
        public string context_source { get { return _context_source; } set { _context_source = value; } }


        //public string context_user_id { get; set; } = string.Empty;
        //public string context_service { get; set; } = string.Empty;
        //public string context_id { get; set; } = string.Empty;
        //public string context_entity_id_name { get; set; } = string.Empty;
        //public string context_name { get; set; } = string.Empty;
        //public string context_message { get; set; } = string.Empty;
    }
}
