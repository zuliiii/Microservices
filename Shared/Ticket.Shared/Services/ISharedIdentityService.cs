using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Shared.Services;

namespace Ticket.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
