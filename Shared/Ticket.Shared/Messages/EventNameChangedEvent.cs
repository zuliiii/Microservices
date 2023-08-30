using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Shared.Messages
{
	public class EventNameChangedEvent
	{
		public string EventId { get; set; }
		public string UpdatedName { get; set; }
	}
}
