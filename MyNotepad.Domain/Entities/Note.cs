using System.ComponentModel.DataAnnotations.Schema;

namespace MyNotepad.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; private set; }

        public Note() { }

        public Note(string title, string content, string color, int userId) 
        {
            Title = title;
            Content = content;
            Color = color;
            UserId = userId;
        }

        public void UpdateTitle(string title) => Title = title;
        public void UpdateContent(string content) => Content = content;
        public void UpdateColor(string color) => Color = color;
    }
}
