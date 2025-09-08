using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;
public class SelecionarContatosQueryHandler(
    IMapper mapper,
    IRepositorioContato repositorioContato,
    ILogger<SelecionarContatosQueryHandler> logger
    ) : IRequestHandler<SelecionarContatosQuery, Result<SelecionarContatosResult>>
{
    public async Task<Result<SelecionarContatosResult>> Handle(SelecionarContatosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registros = query.Quantidade.HasValue ?
                await repositorioContato.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioContato.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarContatosResult>(registros);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registros}.",
                query
                );
            throw;
        }
    }
}
