using MyNotepad.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyNotepad.Domain.Requests
{
    public class NoteRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The 'title' field is required")]
        [Length(1, 120, ErrorMessage = "The length of 'title' field must be from 1 to 120 characters")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2500, ErrorMessage = "The max length of 'description' must be of 2500 characters")]
        public string? Description { get; set; } = string.Empty;
        
        [Required, DefaultValue("blue")]
        public string Color { get; set; } = string.Empty;
    }
}
