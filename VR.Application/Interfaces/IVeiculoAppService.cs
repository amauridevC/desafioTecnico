using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;
using VR.Domain.Enums;

namespace VR.Application.Interfaces
{
    public interface IVeiculoAppService : IAppServiceBase<Veiculo>
    {
        bool TipoVeiculoValido(TipoVeiculo tipo);

        IEnumerable<Veiculo> ObterVeiculosPorModelo(string Modelo);

        IEnumerable<Veiculo> GetAllComFabricante();
        Veiculo GetByIdComFabricante(int id);

        IEnumerable<Veiculo> BuscarPorFabricante(int fabricanteId);

    }
}
