namespace MyNotepad.Domain.DTO;
public class BaseDTO
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }   
}
