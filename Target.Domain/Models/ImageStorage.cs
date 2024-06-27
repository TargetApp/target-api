using System.Reflection.Metadata;

namespace Target.Domain.Models;

public class ImageStorage
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
}
