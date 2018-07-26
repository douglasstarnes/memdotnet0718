using System.Collections.Generic;
using Server.Models.ViewModels;

namespace Server.Models
{
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetConferences();
        Conference GetConferenceById(int id);
        bool CreateConference(ConferenceViewModel conferenceViewModel);
    }
}