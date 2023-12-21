using System;

namespace Target.Domain.Dtos
{
    public class TecnicoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string FormacaoProfissional { get; set; }
        public string AreaAtuacao { get; set; }
        public string RegistroConselho { get; set; }
        public string Descricao { get; set; }
        public string Avaliacao { get; set; }
        public bool EstaExpandido { get; set; }
    }
}
