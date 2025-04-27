namespace Target.Domain.ViewModels;

public class AnaliseMensalViewModel
{
    public string Month { get; set; }
    public int AnalysisAmount { get; set; }
    public int DiseaseAmount{ get; set; }
    public List<MostCommonDiseaseViewModel> MostCommonDiseases { get; set; }
    public List<ReportDateViewModel> ReportDates { get; set; }
}
