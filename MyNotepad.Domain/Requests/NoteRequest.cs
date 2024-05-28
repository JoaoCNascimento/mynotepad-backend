using MyNotepad.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyNotepad.Domain.Requests
{
    public class NoteRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = $"{nameof(Title)} field is required")]
        [StringLength(maximumLength: 120, MinimumLength = 1, ErrorMessage = $"{nameof(Title)} field length must be from 1 to 120 characters")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2500, ErrorMessage = $"{nameof(Description)} field max length must be of 2500 characters")]
        public string? Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = $"{nameof(Color)} field is required"), DefaultValue("blue"), ValidColor]
        public string Color { get; set; } = string.Empty;

        public class ValidColorAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    return ValidationResult.Success; // Assuming null value is handled by [Required] attribute
                }

                if (Enum.TryParse(typeof(DefaultNoteColors), value.ToString(), true, out var result))
                {
                    if (Enum.IsDefined(typeof(DefaultNoteColors), result))
                        return ValidationResult.Success;
                }

                return new ValidationResult($"{value} is not a valid color. Valid colors are: {string.Join(", ", Enum.GetNames(typeof(DefaultNoteColors)))}");
            }
        }
    }
}
