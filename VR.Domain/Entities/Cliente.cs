using System;
using VR.Domain.Interfaces;

namespace VR.Domain.Entities
{
    public class Cliente : ISoftDelete
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DataExclusao { get; set; }
        public string ExcluidoPor { get; set; }
    }
}
