using Ticket.Services.Bakset.DTOs;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Bakset.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> Delete(string userId);

    }
}
