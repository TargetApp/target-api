namespace Target.Domain.Models
{
    public class Tecnico
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfessionalQualification { get; set; }
        public string OccupationArea { get; set; }
        public string CouncilRegistration { get; set; }
        public string Description { get; set; }
        public string Evaluation { get; set; }
    }
}