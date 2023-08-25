namespace Target.Domain.Models
{
    public class Tecnico
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string FormacaoProfissional { get; set; }
        public string AreaAtuacao { get; set; }
        public string RegistroConselho { get; set; }
    }
}