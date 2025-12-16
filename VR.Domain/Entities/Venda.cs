using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Interfaces;

namespace VR.Domain.Entities
{
    public class Venda : ISoftDelete
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Protocolo { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DataExclusao { get; set; }
        public string ExcluidoPor { get; set; }

    }
}
