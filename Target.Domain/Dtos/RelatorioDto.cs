using System;

namespace Target.Domain.Dtos
{
    public class RelatorioDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public int ModelId { get; set; }
        public string? DiseaseName { get; set; }
        public string? Description { get; set; }
        public string? Prevention { get; set; }
        public string? Severity { get; set; }
    }
}
