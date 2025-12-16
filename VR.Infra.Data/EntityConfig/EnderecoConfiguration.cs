using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;

namespace VR.Infra.Data.EntityConfig
{
    public class EnderecoConfiguration : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Rua)
                .IsRequired()
                .HasMaxLength(90);

            Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(36);

            Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(20);

            Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8);

            HasRequired(e => e.FabricanteVeiculo)
                .WithRequiredPrincipal(f => f.Endereco)
                .WillCascadeOnDelete(false);

            ToTable("Enderecos");
        
        }
    }
}
