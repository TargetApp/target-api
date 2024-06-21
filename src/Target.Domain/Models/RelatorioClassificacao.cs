namespace Target.Domain.Models
{
    public class RelatorioClassificacao
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public int ModelId { get; set; }
        public int? DiseaseId { get; set; }
        public int? SeverityId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}