using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Services.Payment.Models;
using Ticket.Shared.ControllerBases;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
