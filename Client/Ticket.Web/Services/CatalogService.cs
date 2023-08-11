using Ticket.Shared.DTOs;
using Ticket.Web.Models;
using Ticket.Web.Models.Catalog;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly HttpClient _httpClient;

		public CatalogService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> CreateEventAsync(EventCreateInput eventCreateInput)
		{
			var response = await _httpClient.PostAsJsonAsync<EventCreateInput>("events", eventCreateInput);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteEventAsync(string eventId)
		{
			var response = await _httpClient.DeleteAsync($"events/{eventId}");

			return response.IsSuccessStatusCode;
		}

		public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
		{
			var response = await _httpClient.GetAsync("categories");

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
			return responseSuccess.Data;
		}

		public async Task<List<EventViewModel>> GetAllEventAsync()
		{
			var response = await _httpClient.GetAsync("events");
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<EventViewModel>>>();
			return responseSuccess.Data;
		}

		public async Task<List<EventViewModel>> GetAllEventByUserIdAsync(string userId)
		{
			var response = await _httpClient.GetAsync($"events/GetAllByUserId/{userId}");

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<EventViewModel>>>();
			return responseSuccess.Data;
		}

		public async Task<EventViewModel> GetByEventId(string eventId)
		{
			var response = await _httpClient.GetAsync($"events/{eventId}");
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<EventViewModel>>();
			return responseSuccess.Data;
		}

		public async Task<bool> UpdateEventAsync(EventUpdateInput eventUpdateInput)
		{
			var response = await _httpClient.PutAsJsonAsync<EventUpdateInput>("events", eventUpdateInput);

			return response.IsSuccessStatusCode;
		}
	}
}
