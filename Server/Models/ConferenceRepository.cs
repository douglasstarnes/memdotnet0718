using System.Collections.Generic;
using System.Linq;
using Server.Models.ViewModels;

namespace Server.Models
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly DataContext ctx;
        public ConferenceRepository(DataContext ctx)
        {
            this.ctx = ctx;

        }

        public bool CreateConference(ConferenceViewModel conferenceViewModel)
        {
            var conference = new Conference {
                Title = conferenceViewModel.Title,
                Date = conferenceViewModel.Date,
                TicketCost = conferenceViewModel.TicketCost
            };
            ctx.Conferences.Add(conference);
            var success = ctx.SaveChanges();
            return success > 0;
        }

        public Conference GetConferenceById(int id)
        {
            var conference = ctx.Conferences.Where(x => x.ConferenceId == id).FirstOrDefault();
            return conference;
        }

        public IEnumerable<Conference> GetConferences()
        {
            var conferences = ctx.Conferences.ToList();
            return conferences;
        }
    }
}