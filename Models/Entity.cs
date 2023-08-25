namespace HAViz.Shared.Models
{
    [Serializable]
    public class Entity
    {
        private string _entity_id = string.Empty;
        private EntityAttributes? _attributes = null;
        private string _id = string.Empty;
        private string _state = string.Empty;
        private DateTime? _last_changed = null;
        private DateTime? _last_updated = null;

        public string entity_id { get { return _entity_id; } set { _entity_id = value; } }
        public EntityAttributes? attributes { get { return _attributes; } set { _attributes = value; } }
        public string id { get { return _id; } set { _id = value; } }
        public string state { get { return _state; } set { _state = value; } }
        public DateTime? last_changed { get { return _last_changed; } set { _last_changed = value; } }
        public DateTime? last_updated { get { return _last_updated; } set { _last_updated = value; } }
    }
    public class EntityAttributes
    {
        private string _friendly_name = string.Empty;
        public string friendly_name { get { return _friendly_name; } set { _friendly_name = value; } }
    }
}
