﻿namespace Ticket.Services.Catalog.DTOs;

public class EventUpdateDto
{
    public string? Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string? UserId { get; set; }
    public decimal Price { get; set; } 
    public string? Picture { get; set; } 
    public string? Location { get; set; } 
    public string CategoryId { get; set; }
}
