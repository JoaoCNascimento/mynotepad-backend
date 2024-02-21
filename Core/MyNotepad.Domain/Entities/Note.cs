using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyNotepad.Domain.Enums;

namespace MyNotepad.Domain.Entities;
public class Note : BaseEntity
{
    [BsonElement("title")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "TitleRequiredErrorMessage")]
    [MaxLength(50, ErrorMessage = "TitleMaxLengthErrorMessage")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("content")]
    [Required(AllowEmptyStrings = true, ErrorMessage = "ContentRequiredErrorMessage")]
    [MaxLength(300000, ErrorMessage = "ContentMaxLengthErrorMessage")]
    public string Content { get; set; } = string.Empty;
    
    [BsonElement("color")]
    [BsonRepresentation(BsonType.String)]
    public Colors Color { get; set; }
}