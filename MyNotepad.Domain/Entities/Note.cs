using System.ComponentModel.DataAnnotations.Schema;

namespace MyNotepad.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; private set; }

        public Note() { }

        public Note(string title, string description, string color, int userId) 
        {
            Title = title;
            Description = description;
            Color = color;
            UserId = userId;
        }

        public void UpdateTitle(string title) => Title = title;
        public void UpdateDescription(string description) => Description = description;
        public void UpdateColor(string color) => Color = color;
    }
}
