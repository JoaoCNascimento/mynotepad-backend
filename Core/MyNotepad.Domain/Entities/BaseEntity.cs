using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyNotepad.Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; protected set; }

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    
    [BsonElement("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

    [BsonElement("__v")]
    public int Version { get; set; }
}