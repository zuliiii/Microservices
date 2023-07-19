using AutoMapper;
using MongoDB.Driver;
using Ticket.Services.Catalog.Models;
using Ticket.Services.Catalog.Settings;

namespace Ticket.Services.Catalog.Services;

public class CategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMongoCollection<Category> categoryCollection, IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);


        _categoryCollection = categoryCollection;
        _mapper = mapper;
    }
}
