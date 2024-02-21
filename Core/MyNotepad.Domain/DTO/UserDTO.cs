namespace MyNotepad.Domain.DTO;
public class UserDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { private get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<NoteDTO> Notes { get; set; }
    public string Status { get; set; }
}
