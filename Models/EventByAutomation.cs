namespace HAViz.Shared.Models
{
    public class EventByAutomation : LogEntry
    {
        public EventByAutomation(LogEntry entry)
        {
            this.when = entry.when;
            this.state = entry.state;
            this.entity_id = entry.entity_id;
            this.context_domain = entry.context_domain;
            this.context_event_type = entry.context_event_type;
            this.context_entity_id = entry.context_entity_id;
            this.context_source = entry.context_source;
        }
    }
}
