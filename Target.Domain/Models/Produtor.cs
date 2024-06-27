namespace Target.Domain.Models
{
    public class Produtor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CoffeeType { get; set; }
        public int PropertySize { get; set; }
        public int Production { get; set; }
    }
}