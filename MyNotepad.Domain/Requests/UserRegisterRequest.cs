using System.ComponentModel.DataAnnotations;

namespace MyNotepad.Domain.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(255)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
