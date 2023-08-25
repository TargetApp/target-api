namespace Target.Domain.Models
{
    public class Produtor
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoCafe { get; set; }
        public int TamanhoPropriedade { get; set; }
        public int Producao { get; set; }
    }
}