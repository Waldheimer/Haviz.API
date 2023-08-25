namespace HAViz.Shared.Models
{
    public class AutomationByEvent : LogEntry
    {

        public AutomationByEvent(LogEntry entry)
        {
            this.when = entry.when;
            this.entity_id = entry.entity_id;
            this.name = entry.name;
            this.domain = entry.domain;
            this.context_entity_id = entry.context_entity_id;
            this.context_state = entry.context_state;
            this.source = entry.source;
        }
    }
}
