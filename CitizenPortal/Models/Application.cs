using System.Reflection.Metadata;

namespace CitizenPortal.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ApplicationType { get; set; } = string.Empty;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Afventer"; //Afventer, Godkendt, Afvist
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
