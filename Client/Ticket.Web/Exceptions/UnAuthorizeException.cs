namespace Ticket.Web.Exceptions
{
	public class UnAuthorizeException: System.Exception
	{
		public UnAuthorizeException() : base() { }

		public UnAuthorizeException(string message) : base(message) { }

		public UnAuthorizeException(string message, Exception innerException) : base(message, innerException) { }
	}
}
