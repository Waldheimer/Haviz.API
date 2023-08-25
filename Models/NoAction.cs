namespace HAViz.Shared.Models
{
    public class NoAction : LogEntry
    {
        public NoAction(LogEntry entry)
        {
            this.when = entry.when;
            this.state = entry.state;
            this.entity_id = entry.entity_id;
        }
    }
}
