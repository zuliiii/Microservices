namespace Ticket.Services.Catalog.DTOs;

public class EventCreateDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string UserId { get; set; }
    public decimal Price { get; set; } 
    public string Picture { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string CategoryId { get; set; }

}
