using AutoMapper;
using Target.Domain.Dtos;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;
using System.Reflection.Metadata;
using Target.Domain.ViewModels;
using System.Globalization;

namespace Target.Application.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioPersist _relatorioPersistence;
        private readonly IGeralPersist _geralPersistence;
        private readonly IImagemPersist _imagemPersistence;
        public RelatorioService(IRelatorioPersist relatorioPersistence, IMapper mapper, IGeralPersist geralPersistence, IImagemPersist imagemPersistence)
        {
            _relatorioPersistence = relatorioPersistence;
            _geralPersistence = geralPersistence;
            _imagemPersistence = imagemPersistence;
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

        public async Task<AnaliseMensalViewModel> ObterAnalisesPorMes(int usuarioId, int mes)
        {
            try
            {
                var relatorios = await _relatorioPersistence.ObterRelatoriosPorUsuarioIdAsync(usuarioId);
                
                if(relatorios != null) 
                {
                    var relatoriosMes = relatorios.FindAll(r => r.CreatedAt?.Month == mes);

                    return new AnaliseMensalViewModel
                    {
                        Month = CultureInfo.GetCultureInfo("pt-BR").TextInfo.ToTitleCase(CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(mes).ToLower()),
                        AnalysisAmount = relatoriosMes.Count(),
                        DiseaseAmount = relatoriosMes.Where(r => r.DiseaseId != 0).Count(),
                        MostCommonDiseases = SelectMostCommonDiseases(relatoriosMes),
                        ReportDates = SelectReportDates(relatoriosMes)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<ReportDateViewModel> SelectReportDates(IEnumerable<RelatorioDto> relatoriosMes)
        {
            var reportDates = relatoriosMes
                .GroupBy(r => r.CreatedAt.GetValueOrDefault().Date)
                .Where(r => r != null)
                .Distinct()
                .Select(r => {
                    return new ReportDateViewModel {
                        Day = r.Key.Day,
                        Month = r.Key.Month,
                        Year = r.Key.Year,
                    };
                })
                .OrderBy(r => r.Day)
                .ToList();
        
            return reportDates;
        }

        private List<MostCommonDiseaseViewModel> SelectMostCommonDiseases(IEnumerable<RelatorioDto> relatoriosMes)
        {
            //total relatorios
            var totalReports = relatoriosMes.Count();

            //quantidade de doencas por relatorio
            var diseaseCounts = relatoriosMes
                .GroupBy(r => {
                    return new {
                        DiseaseId = r.DiseaseId,
                        DiseaseName = r.DiseaseName
                    };
                })
                .Select(r => new
                {
                    DiseaseId = r.Key.DiseaseId,
                    DiseaseName = r.Key.DiseaseName,
                    Count = r.Count()
                })
                .ToList();

            var mostCommmonDiseases = diseaseCounts
                .Select(d => new MostCommonDiseaseViewModel
                {
                    Id = d.DiseaseId.Value,
                    DiseaseName = d.DiseaseName,
                    Percentage = (d.Count * 100) / totalReports
                })
                .OrderByDescending(d => d.Percentage)
                .ToList();

            return mostCommmonDiseases;
        }
    
        public async Task<RelatorioDto> ObterRelatorioPorIdAsync(int relatorioId)
        {
            try
            {
                var maxTries = 10;
                for (int i = 0; i < maxTries; i++)
                {
                    var relatorio = await _relatorioPersistence.ObterRelatorioPorIdAsync(relatorioId);
                    _imagemPersistence.SetImagemBase64(new List<RelatorioDto> { relatorio });
                    
                    if (relatorio.DiseaseName != null) return relatorio;
                    else await Task.Delay(3000);
                }

                return null;
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
                return await _relatorioPersistence.ObterRelatoriosPorUsuarioIdAsync(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosPorDataAsync(int ano, int mes, int dia, int usuarioId)
        {
            try
            {
                var relatorios =  await _relatorioPersistence.ObterRelatoriosPorDataAsync(ano, mes, dia, usuarioId);
                _imagemPersistence.SetImagemBase64(relatorios);

                return relatorios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
