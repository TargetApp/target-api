namespace Target.Domain.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}