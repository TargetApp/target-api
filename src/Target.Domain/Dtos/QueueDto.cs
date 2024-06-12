using Target.Domain.Enum;

namespace Target.Domain.Dtos;

public class QueueDto
{
    public int ImageId { get; set; }
    public int ModelId { get; set; }
    public ModelType ModelType { get; set; }
    public int ReportId { get; set; }
    public byte[] Image { get; set; }
    public bool GenerateMask { get; set; }
}
