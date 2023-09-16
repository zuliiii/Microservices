using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ticket.Web.Services.Interfaces;
using Ticket.Web.Models;
using System.Text;
using Ticket.Web.Models.Order;
using iTextSharp.tool.xml;

namespace Ticket.Web.Services
{
	public class MailService : IMailService
	{
		public void SendHtmlAsPdfToEmail(string toEmail, string subject, string htmlContent)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Encoding utf8 = Encoding.GetEncoding("UTF-8");
			byte[] pdfBytes = GeneratePdfFromHtml(htmlContent, utf8);


				// Prepare the email with the PDF attachment
				MailMessage mailMessage = new MailMessage();
					mailMessage.To.Add(toEmail);
					mailMessage.Subject = subject;
					mailMessage.Body = "Please find the PDF attachment below.";
					mailMessage.From = new MailAddress("zuleyxas@hotmail.com");

			MemoryStream pdfStream = new MemoryStream(pdfBytes);
			mailMessage.Attachments.Add(new Attachment(pdfStream, "invoice.pdf", "application/pdf"));

				// Configure SMTP settings
				SmtpClient smtpClient = new SmtpClient("smtp.outlook.com");
				smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				smtpClient.Port = 587;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("zuleyxas@hotmail.com", "3423010zuli"); 
				smtpClient.EnableSsl = true;


				// Send the email
				smtpClient.Send(mailMessage);
				mailMessage.Dispose();
				smtpClient = null;
		
		}

		public static string CreateInvoiceTemplate(OrderViewModel order)
		{
			var sb = new StringBuilder();

			sb.Append("<!DOCTYPE html>");
			sb.Append("<html>");
			sb.Append("<head>");
			sb.Append("<meta charset='UTF-8'></meta>");
			sb.Append("<title>Invoice</title>");
			sb.Append("<style>");
			sb.Append("/* Define your CSS styles here */");
			sb.Append("/* Example styles; customize as needed */");
			sb.Append("body { font-family: Arial, sans-serif; }");
			sb.Append(".invoice { width: 80%; margin: 0 auto; }");
			sb.Append(".header { text-align: center; }");
			sb.Append(".order-details { margin-top: 20px; }");
			sb.Append(".table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
			sb.Append(".table th, .table td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
			sb.Append(".total { margin-top: 20px; text-align: right; }");
			sb.Append("</style>");
			sb.Append("</head>");
			sb.Append("<body>");
			sb.Append("<div class='invoice'>");
			sb.Append("<div class='header'>");
			sb.Append("<h1>Invoice</h1>");
			sb.Append("</div>");
			sb.Append($"<div class='order-details'><p>Order ID: {order.Id}</p><p>Order Date: {order.CreatedDate}</p></div>");
			sb.Append("<table class='table'>");
			sb.Append("<thead><tr><th>Event</th><th>Price</th><th>Quantity</th><th>Total</th></tr></thead>");
			sb.Append("<tbody>");

			foreach (var item in order.OrderItems)
			{
				sb.Append($"<tr><td>{item.ProductName}</td><td>{item.Price}</td><td>{item.Quantity}</td><td>{item.Price * item.Quantity}</td></tr>");
			}

			sb.Append("</tbody>");
			sb.Append("</table>");
			sb.Append($"<div class='total'><p>Total Price: {order.TotalPrice}</p></div>");
			sb.Append("</div>");
			sb.Append("</body>");
			sb.Append("</html>");

			return sb.ToString();
	}

		static byte[] GeneratePdfFromHtml(string html, Encoding encoding)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				Document document = new Document();
				PdfWriter writer = PdfWriter.GetInstance(document, stream);
				document.Open();

				// Parse HTML content and add it to the PDF with the specified encoding
				using (TextReader reader = new StreamReader(new MemoryStream(encoding.GetBytes(html)), encoding))
				{
					XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);
				}

				document.Close();
				return stream.ToArray();
			}
		}

	}
}
