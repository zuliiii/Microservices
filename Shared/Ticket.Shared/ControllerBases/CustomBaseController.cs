using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Shared.DTOs;

namespace Ticket.Shared.ControllerBases;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode;
        }
    }
}
