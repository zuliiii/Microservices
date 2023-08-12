using Ticket.Web.Models.Basket;

namespace Ticket.Web.Services.Interfaces
{
	public interface IBasketService
	{
		Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);

		Task<BasketViewModel> Get();

		Task<bool> Delete();

		Task AddBasketItem(BasketItemViewModel basketItemViewModel);

		Task<bool> RemoveBasketItem(string eventId);

		Task<bool> ApplyDiscount(string discountCode);

		Task<bool> CancelApplyDiscount();
	}
}
