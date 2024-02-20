namespace MyNotepad.Domain.DTO;
public class NoteDTO : BaseDTO
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}