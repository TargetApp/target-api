namespace Target.Domain.Models
{
    public class Imagens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Filename { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}