namespace HAViz.Shared.Models
{
    public class AutomationStateEntry
    {
        private string _entity_id = string.Empty;
        private string _state = string.Empty;
        private Attributes? _attributes = null;
        public string entity_id { get { return _entity_id; } set { _entity_id = value; } }
        public string state { get { return _state; } set { _state = value; } }
        public Attributes? attributes { get { return _attributes; } set { _attributes = value; } } 
    }
    public class Attributes
    {
        private string _friendly_name = string.Empty;
        private string _id = string.Empty;
        private string _last_triggered = string.Empty;
        public string friendly_name { get { return _friendly_name; } set { _friendly_name= value; } }
        public string id { get { return _id; } set { _id = value; } }
        public string last_triggered { get { return _last_triggered; } set { _last_triggered = value; } }
    }
}
