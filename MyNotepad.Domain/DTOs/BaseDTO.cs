namespace MyNotepad.Domain.DTOs
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
