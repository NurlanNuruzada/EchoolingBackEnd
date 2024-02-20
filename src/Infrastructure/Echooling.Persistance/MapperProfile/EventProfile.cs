using AutoMapper;
using Echooling.Aplication.DTOs.EventDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile;

public class EventProfile:Profile
{
    public EventProfile()
    {
        CreateMap<events,EventCreateDto>().ReverseMap();    
        CreateMap<events, EventGetDto>().ReverseMap();    
        CreateMap<events, GetBouthEventDto>().ReverseMap();    
    }
}
