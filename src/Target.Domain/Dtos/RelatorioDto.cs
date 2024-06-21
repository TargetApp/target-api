using System;

namespace Target.Domain.Dtos
{
    public class RelatorioDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public int ModelId { get; set; }
        public int? DiseaseId { get; set; }
        public int? SeverityId { get; set; }
    }
}
