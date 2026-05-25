namespace CitizenPortal.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Text { get; set; } =string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Application Application { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}
