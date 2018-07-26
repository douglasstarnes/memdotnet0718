using System;

namespace Server.Models
{
    public class Conference
    {
        public int ConferenceId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketCost { get; set; }
    }
}