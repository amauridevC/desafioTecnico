using System;
using System.Collections.Generic;
using VR.Application.DTOs;
using VR.Domain.Entities;


namespace VR.Application.Interfaces
{
    public interface IVendaAppService : IAppServiceBase<Venda>
    {
        bool PrecoVendaValido(Venda venda);
        IEnumerable<VendaListaDto> ObterTodasParaListagem();

        RelatorioVendasMensalDto ObterRelatorioMensal(int? mes = null, int? ano = null);

        Venda ObterPorId(int id);


    }
}
