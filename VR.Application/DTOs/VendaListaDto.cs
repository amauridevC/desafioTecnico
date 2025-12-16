using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Enums;

namespace VR.Application.DTOs
{
    public class VendaListaDto
    {
        public int Id { get; set; }
        public string Protocolo { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal PrecoVenda { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string ModeloVeiculo { get; set; }
        public string NomeFabricante { get; set; }

        public TipoVeiculo TipoVeiculo { get; set; }
    }
}
