namespace Target.Domain.Models
{
    public class Imagens
    {
        public int Id { get; set; }
        public int RelatorioId { get; set; }
        public string Hash { get; set; }
        public string Url { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}