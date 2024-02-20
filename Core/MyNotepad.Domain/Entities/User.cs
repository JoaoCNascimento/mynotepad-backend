using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyNotepad.Domain.Enums;

namespace MyNotepad.Domain.Entities;
public class User : BaseEntity
{
    [BsonElement("name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "NameErrorMessage")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("email")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EmailErrorMessage")]
    [EmailAddress(ErrorMessage = "EmailFormatErrorMessage")]
    public string Email { get; set; } = string.Empty;
    
    [BsonElement("birthDate")]
    public  DateTime BirthDate { get; set; }
    
    [BsonElement("password")]
    [MinLength(6, ErrorMessage = "PasswordLengthError")]
    public string Password { get; set; } = string.Empty;

    [BsonElement("status")]
    [DefaultValue(UserStatus.Pending)]
    [BsonRepresentation(BsonType.String)]
    public UserStatus Status { get; set; }

    [BsonElement("notes")]
    public List<Note> Notes { get; set; }
}