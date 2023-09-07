using System.ComponentModel.DataAnnotations;

namespace Ticket.Web.Models.Catalog
{
    public class EventViewModel
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; }
		public string? UserName { get; set; }
		public decimal Price { get; set; }
        public string? Picture { get; set; }
        public string? StockPictureUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Location { get; set; }
        public string? CategoryId { get; set; }
        public int Quantity { get; set; }

		//public decimal ItemTotalPrice { get; set; }

		private decimal _SubTotal;
		public decimal SubTotal
		{
			get { return _SubTotal; }
			set { _SubTotal = Price * Quantity; }
		}

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
		public DateTime EventDateTime { get; set; }

		public CategoryViewModel Category { get; set; }

        public string? TrimmedCategory => Category.Name.Replace(" ", "");
	}
}
