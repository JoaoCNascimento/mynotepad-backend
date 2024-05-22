using System.ComponentModel.DataAnnotations;

namespace MyNotepad.Domain.Requests
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = $"{nameof(Name)} field is required")]
        [MinLength(3, ErrorMessage = $"{nameof(Name)} field must have at least 3 characters")]
        [MaxLength(255, ErrorMessage = $"{nameof(Name)} field must have the max amount of 255 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = $"{nameof(Email)} field is required")]
        [EmailAddress(ErrorMessage = $"{nameof(Email)} field is invalid")]
        public string Email { get; set; } = string.Empty;


        [MinLength(6, ErrorMessage = $"{nameof(Password)} field must have at least 6 characters")]
        [MaxLength(255, ErrorMessage = $"{nameof(Password)} field must have the max amount of 255 characters")]
        public string Password { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(255)]
        [Compare(nameof(Password), ErrorMessage = "The entered passwords don't match")]
        public string PasswordConfirm { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date field is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
