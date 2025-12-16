using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Interfaces;

namespace VR.Domain.Entities
{
    public class Endereco : ISoftDelete
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public virtual FabricanteVeiculo FabricanteVeiculo { get; set; }
        public int FabricanteVeiculoId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DataExclusao { get; set; }
        public string ExcluidoPor { get; set; }

    }
}
