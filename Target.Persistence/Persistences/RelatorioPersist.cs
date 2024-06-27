using Microsoft.EntityFrameworkCore;
using Target.Domain.Dtos;
using Target.Domain.Models;
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
                    Description = "Os sintomas da doença são visualizados nas folhas mais velhas, com o aparecimento de pequenas manchas amar- ronzadas, com halo amarelado e centro mais claro. As manchas são mais individualizadas e com as bordas bem definidas.",
                    Prevention = "Realizar a rotação de culturas, evitar o plantio de soja em áreas com histórico de ocorrência da doença, utilizar sementes sadias e tratadas, realizar o controle de plantas daninhas e insetos vetores, evitar o plantio de soja em áreas com histórico de ocorrência da doença, utilizar sementes sadias e tratadas, realizar o controle de plantas daninhas e insetos vetores."
                }
            ).AsNoTracking();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosAsync()
        {
            var query = (
                from relatorio in _context.RelatorioClassificacao
                select new RelatorioDto
                {
                    // Nome = relatorio.,
                    // DataCriacao = relatorio.DataCriacao,
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
                select new RelatorioDto
                {
                    Id = relatorio.Id,
                    UserId = relatorio.UserId,
                    ImageId = relatorio.ImageId,
                    ModelId = relatorio.ModelId,
                    DiseaseName = disease.Name,
                    Severity = severity.Description
                }
            ).AsNoTracking();

            return await query.ToListAsync();
        }

    }

}