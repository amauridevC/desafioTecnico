using System;
using System.Collections.Generic;
using VR.Domain.Interfaces;

namespace VR.Domain.Entities
{
    public class FabricanteVeiculo : ISoftDelete
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime AnoFundacao { get; set; }
        public string Website { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DataExclusao { get; set; }
        public string ExcluidoPor { get; set; }
    }
}
