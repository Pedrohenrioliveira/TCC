using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.InscreverClubeCampeonato;

public class InscreverClubeCampeonatoCommand : IRequest<RespostaApi<bool>>
{
    public Guid CampeonatoId { get; set; }
    public Guid ClubeId { get; set; }
    public bool AceitouRegulamento { get; set; }
    public string NomeResponsavel { get; set; } = string.Empty;
    public string TelefoneResponsavel { get; set; } = string.Empty;
    
    // As strings Base64
    public string Base64DocumentoIdentidade { get; set; } = string.Empty;
    public string Base64ComprovantePagamento { get; set; } = string.Empty;
}

public class InscreverClubeCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<InscreverClubeCampeonatoCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(InscreverClubeCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await dbContext.Campeonatos.FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);
        if (campeonato == null)
            return RespostaApi<bool>.Falha("Campeonato não encontrado.");

        var clube = await dbContext.Clubes.FirstOrDefaultAsync(c => c.Id == request.ClubeId, cancellationToken);
        if (clube == null)
            return RespostaApi<bool>.Falha("Clube não encontrado.");

        var jaInscritoOuPendente = await dbContext.InscricoesCampeonatos
            .AnyAsync(i => i.CampeonatoId == request.CampeonatoId && i.ClubeId == request.ClubeId, cancellationToken);

        if (jaInscritoOuPendente)
            return RespostaApi<bool>.Falha("O clube já solicitou inscrição ou está inscrito neste campeonato.");

        if (!request.AceitouRegulamento)
            return RespostaApi<bool>.Falha("O clube deve aceitar o regulamento para solicitar a inscrição.");

        if (string.IsNullOrWhiteSpace(request.NomeResponsavel) || string.IsNullOrWhiteSpace(request.TelefoneResponsavel))
            return RespostaApi<bool>.Falha("Nome e telefone do responsável são obrigatórios.");

        // Process Base64 Files
        var caminhoDoc = SalvarArquivoBase64(request.Base64DocumentoIdentidade, "doc_identidade", clube.Id, campeonato.Id);
        var caminhoComp = SalvarArquivoBase64(request.Base64ComprovantePagamento, "comprovante", clube.Id, campeonato.Id);

        var inscricao = new InscricaoCampeonato
        {
            CampeonatoId = request.CampeonatoId,
            ClubeId = request.ClubeId,
            AceitouRegulamento = request.AceitouRegulamento,
            NomeResponsavel = request.NomeResponsavel,
            TelefoneResponsavel = request.TelefoneResponsavel,
            CaminhoDocumentoIdentidade = caminhoDoc,
            CaminhoComprovantePagamento = caminhoComp,
            Status = StatusInscricao.Pendente,
            DataSolicitacao = DateTime.UtcNow
        };

        dbContext.InscricoesCampeonatos.Add(inscricao);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Solicitação de inscrição enviada com sucesso e aguardando aprovação.");
    }

    private string SalvarArquivoBase64(string base64, string prefixo, Guid clubeId, Guid campeonatoId)
    {
        if (string.IsNullOrWhiteSpace(base64)) return string.Empty;

        try
        {
            // Remover header do base64 (ex: data:image/png;base64,)
            var commaIndex = base64.IndexOf(',');
            var base64Data = commaIndex > 0 ? base64.Substring(commaIndex + 1) : base64;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "inscricoes");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var ext = ".png"; // Simplificando para TCC
            if (base64.StartsWith("data:image/jpeg")) ext = ".jpg";
            else if (base64.StartsWith("data:application/pdf")) ext = ".pdf";

            var fileName = $"{prefixo}_{clubeId}_{campeonatoId}_{DateTime.Now.Ticks}{ext}";
            var fullPath = Path.Combine(uploadPath, fileName);

            File.WriteAllBytes(fullPath, Convert.FromBase64String(base64Data));

            return $"/uploads/inscricoes/{fileName}";
        }
        catch
        {
            return string.Empty;
        }
    }
}
