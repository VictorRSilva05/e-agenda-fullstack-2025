using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloCompromisso;
using eAgenda.Core.Dominio.ModuloContato;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.AutoMapper;
public class ContatoMappingProfile : Profile
{
    public ContatoMappingProfile()
    {
        CreateMap<CadastrarContatoCommand, Contato>();
        CreateMap<Contato, CadastrarContatoCommand>();

        CreateMap<EditarContatoCommand, Contato>();
        CreateMap<Contato, EditarContatoCommand>();

        CreateMap<Contato, SelecionarContatoPorIdResult>()
            .ConvertUsing(src => new SelecionarContatoPorIdResult(
                src.Id,
                src.Nome,
                src.Telefone,
                src.Email,
                src.Empresa,
                src.Cargo,
                (src.Compromissos ?? Enumerable.Empty<Compromisso>())
                    .Select(r => new DetalhesCompromissoContatoDto(
                    r.Assunto,
                    r.Data,
                    r.HoraInicio,
                    r.HoraTermino
                    )).ToImmutableList()
                ));

        CreateMap<Contato, SelecionarContatosDto>();

        CreateMap<IEnumerable<Contato>, SelecionarContatosResult>()
            .ConvertUsing((src, dest, ctx) =>
                new SelecionarContatosResult(
                    src?.Select(c => ctx.Mapper.Map<SelecionarContatosDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarContatosDto>.Empty
            ));
    }
}
