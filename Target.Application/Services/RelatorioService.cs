using AutoMapper;
using Target.Domain.Dtos;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioPersist _relatorioPersistence;
        private readonly IMapper _mapper;
        private readonly IGeralPersist _geralPersistence;
        public RelatorioService(IRelatorioPersist relatorioPersistence, IMapper mapper, IGeralPersist geralPersistence)
        {
            _relatorioPersistence = relatorioPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }

        public async Task<RelatorioClassificacao> AdicionarRelatorioAsync(RelatorioCriarDto relatorioDto, int usuarioId)
        {
            try
            {
                var relatorio = new RelatorioClassificacao
                {
                    UserId = usuarioId,

                };

                _geralPersistence.Add(relatorio);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var relatorioRetorno = await _relatorioPersistence.ObterRelatorioPorIdAsync(relatorio.Id);
                    return relatorio;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioDto> AtualizarRelatorioAsync(int relatorioId, RelatorioDto relatorioDto)
        {
            try
            {
                var relatorio = await _relatorioPersistence.ObterRelatorioPorIdAsync(relatorioId);
                if (relatorio == null) return null;
                
                //relatorio. = relatorioDto.Name;

                _geralPersistence.Update(relatorio);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    return relatorio;
                }
                else
                {
                    return null;
                }   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirRelatorioAsync(int relatorioId)
        {
            try
            {
                var relatorio = await _relatorioPersistence.ObterRelatorioPorIdAsync(relatorioId);
                if (relatorio == null) return false;

                _geralPersistence.Delete(relatorio);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> InsertClassificationReport(int userId, int imageId, int modelId)
        {
            try
            {
                var relatorio = new RelatorioClassificacao
                {
                    UserId = userId,
                    ImageId = imageId,
                    ModelId = modelId,
                    CreatedAt = DateTime.Now
                };

                _geralPersistence.Add(relatorio);
                await _geralPersistence.SaveChangesAsync();

                return relatorio.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioDto> ObterRelatorioPorIdAsync(int relatorioId)
        {
            try
            {
                return await _relatorioPersistence.ObterRelatorioPorIdAsync(relatorioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var relatorios = await _relatorioPersistence.ObterRelatoriosPorUsuarioIdAsync(usuarioId);
                if (relatorios == null) return null;

                return relatorios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
