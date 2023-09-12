using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ThirdParty.Json.LitJson;

namespace Ticket.Services.Catalog.Models;

public class Event
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public string? UserId { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public string? Picture { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }
	[BsonRepresentation(BsonType.DateTime)]
	public DateTime EventDateTime { get; set; }
    public string? Location { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public string? CategoryId { get; set; }

    [BsonIgnore]
    public Category Category { get; set; }
}
