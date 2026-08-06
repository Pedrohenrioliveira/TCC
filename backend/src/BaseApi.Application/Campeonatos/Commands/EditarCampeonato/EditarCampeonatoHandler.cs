using BaseApi.Application.Comum.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Campeonatos.Commands.EditarCampeonato;

public class EditarCampeonatoHandler : IRequestHandler<EditarCampeonatoCommand, bool>
{
    private readonly IAppDbContext _context;

    public EditarCampeonatoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(EditarCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await _context.Campeonatos
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (campeonato == null)
            return false;

        campeonato.Nome = request.Nome;
        campeonato.Local = request.Local;
        campeonato.DataInicio = request.DataInicio;
        campeonato.DataFim = request.DataFim;
        campeonato.LimiteEquipes = request.LimiteEquipes;
        campeonato.TaxaInscricao = request.TaxaInscricao;
        campeonato.ChavePix = request.ChavePix;
        campeonato.DiasDosJogos = request.DiasDosJogos;
        campeonato.Descricao = request.Descricao;
        campeonato.AtualizadoEm = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(request.Base64ImagemCampo))
        {
            var caminhoImgCampo = SalvarArquivoBase64(request.Base64ImagemCampo, "campo", campeonato.Id);
            if (!string.IsNullOrEmpty(caminhoImgCampo))
            {
                campeonato.CaminhoImagemCampo = caminhoImgCampo;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
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
