using Ticket.Services.Catalog.DTOs;
using Ticket.Services.Catalog.Models;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}
