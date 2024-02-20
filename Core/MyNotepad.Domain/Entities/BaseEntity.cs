using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyNotepad.Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; protected set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
}