namespace Target.Domain.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int RegisterTypeId { get; set; }
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime TokenUpdatedAt { get; set; }
        public string TokenLogin { get; set; }
        public int TokenAttempts { get; set; }
    }
}