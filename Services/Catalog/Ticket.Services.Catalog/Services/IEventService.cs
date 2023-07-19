using AutoMapper;
using MongoDB.Driver;
using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Models;
using Ticket.Services.Catalog.Settings;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Catalog.Services;

public interface IEventService
{

    Task<Response<List<EventDto>>> GetAllAsync();
     Task<Response<EventDto>> GetByIdAsync(string id);
     Task<Response<List<EventDto>>> GetResponseAsync(string userId);
     Task<Response<EventDto>> CreateAsync(EventCreateDto eventCreateDto);
     Task<Response<NoContent>> UpdateAsync(EventUpdateDto eventUpdateDto);
    Task<Response<NoContent>> DeleteAsync(string id);
 

}
