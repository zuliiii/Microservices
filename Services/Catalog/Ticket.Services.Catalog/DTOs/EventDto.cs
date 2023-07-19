﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Ticket.Services.Catalog.Models;

namespace Ticket.Services.Catalog.DTOs;

public class EventDto
{
    
    public string Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string UserId { get; set; }
    public decimal Price { get; set; } 
    public string Picture { get; set; } = null!;
    public DateTime CreatedTime { get; set; } 
    public string Location { get; set; } = null!;
    public string CategoryId { get; set; } = null!;


    public CategoryDto Category { get; set; }
}
