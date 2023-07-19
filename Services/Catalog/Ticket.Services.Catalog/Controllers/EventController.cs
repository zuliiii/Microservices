using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Services;
using Ticket.Shared.ControllerBases;

namespace Ticket.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : CustomBaseController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;

    }

    public async Task<IActionResult> GetAll()
    {
        var response = await _eventService.GetAllAsync();
        return CreateActionResultInstance(response);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _eventService.GetByIdAsync(id);

        return CreateActionResultInstance(response);

    }

    [Route("/api/[controller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        var response = await _eventService.GetByIdAsync(userId);

        return CreateActionResultInstance(response);

    }

    [HttpPost]
    public async Task<IActionResult> Create(EventCreateDto eventCreateDto)
    {
        var response = await _eventService.CreateAsync(eventCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(EventUpdateDto eventUpdateDto)
    {
        var response = await _eventService.UpdateAsync(eventUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _eventService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }

}
