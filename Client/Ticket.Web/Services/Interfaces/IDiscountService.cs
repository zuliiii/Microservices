using Ticket.Web.Models.Discount;

namespace Ticket.Web.Services.Interfaces
{
	public interface IDiscountService
	{
		Task<DiscountViewModel> GetDiscount(string discountCode);

	}
}
