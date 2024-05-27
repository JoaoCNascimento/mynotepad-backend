namespace MyNotepad.Domain.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public List<NoteDTO> Notes { get; set; } = new();

        public UserDTO() { }
    }
}
