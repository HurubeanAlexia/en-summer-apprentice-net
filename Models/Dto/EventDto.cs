namespace endavaPractica.Net.Models.Dto
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = String.Empty;
        public string EventDescription { get; set; }
        
        public string EventType { get; set; }
        public string Venue { get; set; }

    }
}
