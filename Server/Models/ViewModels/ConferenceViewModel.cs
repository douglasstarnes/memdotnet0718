using System;

namespace Server.Models.ViewModels
{
    public class ConferenceViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketCost { get; set; }
    }
}