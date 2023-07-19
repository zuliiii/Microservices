using AutoMapper;
using MongoDB.Driver;
using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Models;
using Ticket.Services.Catalog.Settings;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }


    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        // get all categories
        var categories = await _categoryCollection.Find(ctg => true).ToListAsync();

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), StatusCodes.Status200OK);

    }

    public async Task<Response<CategoryDto>> CreateAsync(Category category)
    {
         await _categoryCollection.InsertOneAsync(category);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), StatusCodes.Status204NoContent);  

    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

        if(category == null)
        {
            return Response<CategoryDto>.Fail("fail", StatusCodes.Status404NotFound);
        }
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), StatusCodes.Status200OK);
    }
}
