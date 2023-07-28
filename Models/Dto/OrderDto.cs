namespace endavaPractica.Net.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public DateTime? OrderedAt { get; set; }

        public string TicketCategory { get; set; }

        public int NumberOfTickets { get; set; }

        public float TotalPrice { get; set; }

    }
}
  