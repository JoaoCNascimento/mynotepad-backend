namespace MyNotepad.Domain.DTOs
{
    public class NoteDTO : BaseDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color {  get; set; } = string.Empty;
        public int UserId { get; set; }
        public NoteDTO() { }
    }
}
