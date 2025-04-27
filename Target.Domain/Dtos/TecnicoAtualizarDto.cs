using System;

namespace Target.Domain.Dtos
{
    public class TecnicoAtualizarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ProfessionalQualification { get; set; }
        public string OccupationArea { get; set; }
        public string CouncilRegistration { get; set; }
        public string Description { get; set; }
        public double NewEvaluation { get; set; }
        public bool IsExpanded { get; set; }
    }
}
