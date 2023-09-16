namespace Ticket.Web.Services.Interfaces
{
	public interface IMailService
	{
		void SendHtmlAsPdfToEmail(string toEmail, string subject, string htmlContent);
	}
}
