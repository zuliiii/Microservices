using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Services.PhotoStock.DTOs;
using Ticket.Shared.ControllerBases;
using Ticket.Shared.DTOs;

namespace Ticket.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo !=null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, StatusCodes.Status200OK));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", StatusCodes.Status400BadRequest));
        }
    }
}
