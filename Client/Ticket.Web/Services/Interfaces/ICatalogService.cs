using Ticket.Web.Models.Catalog;

namespace Ticket.Web.Services.Interfaces
{
	public interface ICatalogService
	{
		Task<List<EventViewModel>> GetAllEventAsync();

		Task<List<CategoryViewModel>> GetAllCategoryAsync();

		//Task<List<EventViewModel>> GetAllEventByUserIdAsync(string userId);

		Task<EventViewModel> GetByEventId(string eventId);

		Task<bool> CreateEventAsync(EventCreateInput eventCreateInput);

		Task<bool> UpdateEventAsync(EventCreateInput eventUpdateInput);

		Task<bool> DeleteEventAsync(string eventId);
	}
}
