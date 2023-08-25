namespace HAViz.Shared.Models
{
    public class EntityState
    {
        private string _id;
        public string entity_id { get { return _id; } set { _id = value; } }
        private string _state;

        public string state
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}
