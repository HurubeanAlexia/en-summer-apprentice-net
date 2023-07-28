using endavaPractica.Net.Models;
using endavaPractica.Net.Models.Dto;
using AutoMapper;


namespace endavaPractica.Net.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
