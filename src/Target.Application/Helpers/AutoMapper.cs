using AutoMapper;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UsuarioDto, Usuarios>().ReverseMap();
            CreateMap<UsuarioLoginDto, Usuarios>().ReverseMap();
            CreateMap<TecnicoDto, Tecnico>().ReverseMap();
            CreateMap<RelatorioDto, Relatorio>().ReverseMap();
        }
    }
}
