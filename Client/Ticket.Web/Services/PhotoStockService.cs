using Ticket.Web.Models.PhotoStock;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class PhotoStockService : IPhotoStockService
	{
		public Task<bool> DeletePhoto(string photoUrl)
		{
			throw new NotImplementedException();
		}

		public Task<PhotoViewModel> UploadPhoto(IFormFile photo)
		{
			throw new NotImplementedException();
		}
	}
}
