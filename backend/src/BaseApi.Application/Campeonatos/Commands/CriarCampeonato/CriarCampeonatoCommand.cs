using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;

namespace BaseApi.Application.Campeonatos.Commands.CriarCampeonato;

public class CriarCampeonatoCommand : IRequest<RespostaApi<Guid>>
{
    public string Nome { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int LimiteEquipes { get; set; }
    public string CaminhoLogo { get; set; } = string.Empty;
    public decimal TaxaInscricao { get; set; }
    public string ChavePix { get; set; } = string.Empty;
    public string DiasDosJogos { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Base64ImagemCampo { get; set; } = string.Empty;
}

public class CriarCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<CriarCampeonatoCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(CriarCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var caminhoImgCampo = SalvarArquivoBase64(request.Base64ImagemCampo, "campo", Guid.NewGuid());

        var campeonato = new Campeonato
        {
            Nome = request.Nome,
            Local = request.Local,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim,
            LimiteEquipes = request.LimiteEquipes,
            CaminhoLogo = request.CaminhoLogo,
            TaxaInscricao = request.TaxaInscricao,
            ChavePix = request.ChavePix,
            DiasDosJogos = request.DiasDosJogos,
            Descricao = request.Descricao,
            CaminhoImagemCampo = caminhoImgCampo,
            Status = StatusCampeonato.Aberto
        };

        dbContext.Campeonatos.Add(campeonato);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(campeonato.Id, "Campeonato criado com sucesso.");
    }

    private string SalvarArquivoBase64(string base64, string prefixo, Guid id)
    {
        if (string.IsNullOrWhiteSpace(base64)) return string.Empty;

        try
        {
            var commaIndex = base64.IndexOf(',');
            var base64Data = commaIndex > 0 ? base64.Substring(commaIndex + 1) : base64;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "campeonatos");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var ext = ".png";
            if (base64.StartsWith("data:image/jpeg")) ext = ".jpg";

            var fileName = $"{prefixo}_{id}_{DateTime.Now.Ticks}{ext}";
            var fullPath = Path.Combine(uploadPath, fileName);

            File.WriteAllBytes(fullPath, Convert.FromBase64String(base64Data));

            return $"/uploads/campeonatos/{fileName}";
        }
        catch
        {
            return string.Empty;
        }
    }
}
