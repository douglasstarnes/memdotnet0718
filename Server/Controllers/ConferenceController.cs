using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceRepository conferenceRepository;
        public ConferenceController(IConferenceRepository conferenceRepository)
        {
            this.conferenceRepository = conferenceRepository;
        }

        // [HttpGet]
        // public IActionResult GetConferences()
        // {
        //     return Ok(conferenceRepository.GetConferences());
        // }
        [HttpGet]
        public IActionResult GetConferences()
        {
            return Ok(conferenceRepository.GetConferences().Select(conference => new {
                ConferenceId = conference.ConferenceId,
                Title = conference.Title
            }));
        }

        [HttpGet("{id}")]
        public IActionResult GetConferenceById(int id)
        {
            var conference = conferenceRepository.GetConferenceById(id);
            if (conference == null)
                return NotFound();
            return Ok(conference);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateConference(ConferenceViewModel conferenceViewModel)
        {
            var success = conferenceRepository.CreateConference(conferenceViewModel);
            if (success)
                return Created("pending", null);
            return BadRequest(new {Error = "User exists" });
        }
    }
}