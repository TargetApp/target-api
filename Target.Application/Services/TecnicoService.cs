using System;
using AutoMapper;
using Target.Domain.Dtos;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly ITecnicoPersist _tecnicoPersist;
        private readonly IGeralPersist _geralPersist;
        public TecnicoService(ITecnicoPersist tecnicoPersist, IGeralPersist geralPersist)
        {
            _geralPersist = geralPersist;
            _tecnicoPersist = tecnicoPersist;
        }

        public async Task<Tecnico> AdicionarTecnicoAsync(TecnicoDto model, int usuarioId)
        {
            try
            {   
                var tecnico = new Tecnico
                {
                    UserId = usuarioId,
                    ProfessionalQualification = model.ProfessionalQualification,
                    OccupationArea = model.OccupationArea,
                    CouncilRegistration = model.CouncilRegistration,
                    Description = model.Description,
                    Evaluation = model.Evaluation,
                    TotalEvaluations = model.Evaluation != null ? 1 : 0
                };
                
                _geralPersist.Add(tecnico);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return tecnico;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tecnico> AtualizarTecnicoAsync(int id, TecnicoAtualizarDto model, bool isEvaluation = false)
        {
            try
            {
                var tecnico = await _tecnicoPersist.ObterTecnicoPorIdAsync(id);
                if (tecnico == null) return null;

                tecnico.ProfessionalQualification = model.ProfessionalQualification;
                tecnico.OccupationArea = model.OccupationArea;
                tecnico.CouncilRegistration = model.CouncilRegistration;
                tecnico.Description = model.Description;
                if(isEvaluation)
                {
                    tecnico.TotalEvaluations += 1;
                    tecnico.Evaluation = CalcularAvaliacao(
                                            Convert.ToDouble(tecnico.Evaluation),
                                            model.NewEvaluation, 
                                            tecnico.TotalEvaluations.Value
                                        );
                }

                _geralPersist.Update(tecnico);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return tecnico;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CalcularAvaliacao(double mediaAtual, double novaAvaliacao, int totalAvaliacao)
        {
            var maxAvaliacao = 5;
            novaAvaliacao = novaAvaliacao > maxAvaliacao ? maxAvaliacao : novaAvaliacao;

            var avaliacaoAnterior = mediaAtual * (totalAvaliacao - 1);
            var avaliacao = (avaliacaoAnterior + novaAvaliacao) / totalAvaliacao;

            avaliacao = Math.Round(avaliacao, 1, MidpointRounding.AwayFromZero);
            avaliacao = Math.Truncate(avaliacao * 100) / 100;

            return avaliacao >= 5 ? maxAvaliacao.ToString() : avaliacao.ToString();
        }

        public async Task<bool> ExcluirTecnicoAsync(int id)
        {
            try
            {
                var tecnico = await _tecnicoPersist.ObterTecnicoPorIdAsync(id);
                if (tecnico == null) throw new Exception("Técnico não encontrado.");

                _geralPersist.Delete(tecnico);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TecnicoDto>> ObterListaTecnicosAsync(string filter)
        {
            try
            {
                var tecnicos = await _tecnicoPersist.ObterListaTecnicosAsync(filter);
                if (tecnicos == null) return null;

                return tecnicos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tecnico> ObterTecnicoAsyncById(int id)
        {
            try
            {
                var tecnico = await _tecnicoPersist.ObterTecnicoPorIdAsync(id);
                if (tecnico == null) return null;

                return tecnico;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
