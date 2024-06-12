using System;

namespace Target.Domain.Dtos
{
    public class UsuarioUpdateDto
    {
        public int RegisterTypeId { get; set; }
        public int AccountTypeId { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
