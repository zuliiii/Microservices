using AutoMapper;
using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Models;

namespace Ticket.Services.Catalog.Mapping;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Event, EventCreateDto >().ReverseMap();
        CreateMap<Event, EventUpdateDto> ().ReverseMap();   
    }
}
