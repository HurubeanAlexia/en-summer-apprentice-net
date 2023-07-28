using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using endavaPractica.Net.Models.Dto;
using Microsoft.Extensions.Logging;
using endavaPractica.Net.Models;
using endavaPractica.Net.Repository;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace endavaPractica.Net.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = events.Select(e => new EventDto()
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.VenueLocation ?? string.Empty
            });


            return Ok(dtoEvents);
        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(long id)
        {
            var @event = await _eventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            if (eventEntity == null)
            {
                return NotFound();
            }
            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            _eventRepository.Update(eventEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }



    }
}