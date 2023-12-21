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
                    UsuarioId = usuarioId,
                    FormacaoProfissional = model.FormacaoProfissional,
                    AreaAtuacao = model.AreaAtuacao,
                    RegistroConselho = model.RegistroConselho,
                    Descricao = model.Descricao,
                    Avaliacao = model.Avaliacao
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

        public async Task<Tecnico> AtualizarTecnicoAsync(int id, TecnicoDto model)
        {
            try
            {
                var tecnico = await _tecnicoPersist.ObterTecnicoPorIdAsync(id);
                if (tecnico == null) return null;

                tecnico.FormacaoProfissional = model.FormacaoProfissional;
                tecnico.AreaAtuacao = model.AreaAtuacao;
                tecnico.RegistroConselho = model.RegistroConselho;
                tecnico.Descricao = model.Descricao;
                tecnico.Avaliacao = model.Avaliacao;

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

        public async Task<List<TecnicoDto>> ObterListaTecnicosAsync()
        {
            try
            {
                var tecnicos = await _tecnicoPersist.ObterListaTecnicosAsync();
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
