using System;
using System.Collections.Generic;
using VR.Domain.Enums;
using VR.Domain.Interfaces;

namespace VR.Domain.Entities
{
    public class Veiculo : ISoftDelete
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public DateTime AnoFabricacao { get; set; }
        public Decimal Preco { get; set; }
        public TipoVeiculo TipoDeVeiculo { get; set; }
        public string Descricao { get; set; }
        public int FabricanteVeiculoId { get; set; }
        public virtual FabricanteVeiculo Fabricante { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DataExclusao { get; set; }
        public string ExcluidoPor { get; set; }

    }
}
