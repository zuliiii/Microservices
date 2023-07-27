using Ticket.Services.Bakset.DTOs;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Bakset.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(BasketDto basketDto);
        Task<Response<BasketDto>> SaveOrUpdate(string userId);
        Task<Response<BasketDto>> Delete(string userId);

    }
}
