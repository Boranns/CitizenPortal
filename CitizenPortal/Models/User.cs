using static System.Net.Mime.MediaTypeNames;

namespace CitizenPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; //Borger, Sagsbehandler, Admin.
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
