namespace Target.Domain.Models;

public class TargetQueue
{
    public int Id { get; set; }
    public int ImageId { get; set; }
    public int ModelId { get; set; }
    public int ClassificationReportId { get; set; }
    public int? SegmentationReportId { get; set; }
    public byte[] Image { get; set; }
    public bool? GenerateMask { get; set; }
}
