using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Models;
using Ticket.Services.Catalog.Settings;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Catalog.Services;

public class EventService: IEventService
{
    private readonly IMongoCollection<Event> _eventCollection;
    private readonly IMongoCollection<Category> _categoryCollection;

    private readonly IMapper _mapper;

    public EventService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _eventCollection = database.GetCollection<Event>(databaseSettings.EventCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<EventDto>>> GetAllAsync()
    {
        var events = await _eventCollection.Find(evt => true).ToListAsync();

        if (events.Any())
        {
            // Get all categoryIds from events to fetch categories in a single query
            var categoryIds = events.Select(evt => evt.CategoryId).Distinct().ToList();

            // Fetch all categories in a single query using categoryIds
            var categories = await _categoryCollection.Find<Category>(x => categoryIds.Contains(x.Id)).ToListAsync();
            var categoryDictionary = categories.ToDictionary(cat => cat.Id);

            // Assign categories to events using the dictionary
            foreach (var eventt in events)
            {
                if (categoryDictionary.TryGetValue(eventt.CategoryId, out var category))
                {
                    eventt.Category = category;
                }
            }
        }
        else
        {
            events = new List<Event>();
        }

        return Response<List<EventDto>>.Success(_mapper.Map<List<EventDto>>(events), StatusCodes.Status200OK);
    }

    public async Task<Response<EventDto>> GetByIdAsync(string id)
    {
        var eventt = await _eventCollection.Find<Event>(x=>x.Id==id).FirstOrDefaultAsync();

       if (eventt ==null)
       {
            return Response<EventDto>.Fail("course not found", StatusCodes.Status404NotFound);
       }
        eventt.Category = await _categoryCollection.Find<Category>(x=>x.Id == eventt.CategoryId).FirstOrDefaultAsync();
        return Response<EventDto>.Success(_mapper.Map<EventDto>(eventt), StatusCodes.Status200OK);

}

    public async Task<Response<List<EventDto>>> GetResponseAsync(string userId)
    {
        var events = await _eventCollection.Find<Event>(x=>x.UserId==userId).ToListAsync();

        if (events.Any())
        {
            // Get all categoryIds from events to fetch categories in a single query
            var categoryIds = events.Select(evt => evt.CategoryId).Distinct().ToList();

            // Fetch all categories in a single query using categoryIds
            var categories = await _categoryCollection.Find<Category>(x => categoryIds.Contains(x.Id)).ToListAsync();
            var categoryDictionary = categories.ToDictionary(cat => cat.Id);

            // Assign categories to events using the dictionary
            foreach (var eventt in events)
            {
                if (categoryDictionary.TryGetValue(eventt.CategoryId, out var category))
                {
                    eventt.Category = category;
                }
            }
        }
        else
        {
            events = new List<Event>();
        }

        return Response<List<EventDto>>.Success(_mapper.Map<List<EventDto>>(events), StatusCodes.Status200OK);
    }

    public async Task<Response<EventDto>> CreateAsync(EventCreateDto eventCreateDto)
    {
        var newEvent = _mapper.Map<Event>(eventCreateDto);

        newEvent.CreatedTime = DateTime.Now;
        await _eventCollection.InsertOneAsync(newEvent);

        return Response<EventDto>.Success(_mapper.Map<EventDto>(newEvent), StatusCodes.Status200OK);
    }

    public async Task<Response<NoContent>> UpdateAsync(EventUpdateDto eventUpdateDto)
    {
        var updateEvent = _mapper.Map<Event>(eventUpdateDto);

        var result = await _eventCollection.FindOneAndReplaceAsync(x => x.Id == eventUpdateDto.Id, updateEvent);

        if (result == null)
        {
            return Response<NoContent>.Fail("Event NOT Found", StatusCodes.Status404NotFound);
        }
        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _eventCollection.DeleteOneAsync(x=>x.Id == id);

        if (result.DeletedCount >0 )
        {
            return Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }
        else
        {
            return Response<NoContent>.Fail("Event NOT Found", StatusCodes.Status404NotFound);
        }
    }

}
