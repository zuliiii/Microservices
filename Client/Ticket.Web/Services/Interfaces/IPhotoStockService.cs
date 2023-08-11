using Ticket.Web.Models.PhotoStock;

namespace Ticket.Web.Services.Interfaces
{
	public interface IPhotoStockService
	{
		Task<PhotoViewModel> UploadPhoto(IFormFile photo);

		Task<bool> DeletePhoto(string photoUrl);
	}
}
