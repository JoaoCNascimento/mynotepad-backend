using System.ComponentModel.DataAnnotations;

namespace MyNotepad.Domain.Requests
{
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = $"{nameof(Email)} field is invalid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = $"{nameof(Password)} field is required")]
        public string Password { get; set; } = string.Empty;
    }
}
