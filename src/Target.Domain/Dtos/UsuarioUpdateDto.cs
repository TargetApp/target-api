using System;

namespace Target.Domain.Dtos
{
    public class UsuarioUpdateDto
    {
        public string TipoCadastro { get; set; }
        public string TipoConta { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
    }
}
