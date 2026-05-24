namespace CitizenPortal.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        public Application Application { get; set; } = null!;
    }
}
