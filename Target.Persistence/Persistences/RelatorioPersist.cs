using Microsoft.EntityFrameworkCore;
using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Domain.ViewModels;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class RelatorioPersist : IRelatorioPersist
    {
        private readonly TargetDbContext _context;

        public RelatorioPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<RelatorioDto> ObterRelatorioPorIdAsync(int id)
        {
            var query = (
                from relatorio in _context.RelatorioClassificacao
                where relatorio.Id == id
                join disease in _context.Doencas on relatorio.DiseaseId equals disease.Id into diseases
                from disease in diseases.DefaultIfEmpty()
                join severity in _context.Severidade on relatorio.SeverityId equals severity.Id into severities
                from severity in severities.DefaultIfEmpty()
                select new RelatorioDto {
                    Id = relatorio.Id,
                    UserId = relatorio.UserId,
                    ImageId = relatorio.ImageId,
                    ModelId = relatorio.ModelId,
                    DiseaseName = disease.Name.ToUpper(),
                    Severity = severity.Description,
                    Treatment = disease.Treatment,
                    Description = disease.Description,
                    Prevention = disease.Prevention,
                    CreatedAt = relatorio.CreatedAt
                }
            ).AsNoTracking();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosPorDataAsync(int ano, int mes, int dia, int usuarioId)
        {
            var query = (
                from relatorio in _context.RelatorioClassificacao
                where relatorio.UserId == usuarioId &&
                      relatorio.CreatedAt.Value.Year == ano && 
                      relatorio.CreatedAt.Value.Month == mes && 
                      relatorio.CreatedAt.Value.Day == dia
                join disease in _context.Doencas on relatorio.DiseaseId equals disease.Id into diseases
                from disease in diseases.DefaultIfEmpty()
                join severity in _context.Severidade on relatorio.SeverityId equals severity.Id into severities
                from severity in severities.DefaultIfEmpty()
                select new RelatorioDto {
                    Id = relatorio.Id,
                    UserId = relatorio.UserId,
                    ImageId = relatorio.ImageId,
                    ModelId = relatorio.ModelId,
                    DiseaseId = relatorio.DiseaseId,
                    DiseaseName = disease.Name.ToUpper(),
                    Severity = severity.Description,
                    Treatment = disease.Treatment,
                    Description = disease.Description,
                    Prevention = disease.Prevention,
                    CreatedAt = relatorio.CreatedAt
                }
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId)
        {
            var query = (
                from relatorio in _context.RelatorioClassificacao
                where relatorio.UserId == usuarioId
                join disease in _context.Doencas on relatorio.DiseaseId equals disease.Id into diseases
                from disease in diseases.DefaultIfEmpty()
                join severity in _context.Severidade on relatorio.SeverityId equals severity.Id into severities
                from severity in severities.DefaultIfEmpty()
                select new RelatorioDto {
                    Id = relatorio.Id,
                    UserId = relatorio.UserId,
                    ImageId = relatorio.ImageId,
                    ModelId = relatorio.ModelId,
                    DiseaseId = relatorio.DiseaseId,
                    DiseaseName = disease.Name.ToUpper(),
                    Severity = severity.Description,
                    Treatment = disease.Treatment,
                    Description = disease.Description,
                    Prevention = disease.Prevention,
                    CreatedAt = relatorio.CreatedAt
                }
            ).AsNoTracking();

            return await query.ToListAsync();
        }

    }

}