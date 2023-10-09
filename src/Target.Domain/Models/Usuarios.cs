namespace Target.Domain.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? TipoCadastro { get; set; }
        public string? TipoConta { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }
        public string? TokenLogin { get; set; }
        public int? TokenTentativas { get; set; }
        public DateTime? DataAtualizacaoToken { get; set; }
        public DateTime? DataCriacaoUsuario { get; set; }
        public DateTime? DataAtualizacaoUsuario { get; set; }
    }
}