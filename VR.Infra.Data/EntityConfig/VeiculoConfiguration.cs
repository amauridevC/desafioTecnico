using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;

namespace VR.Infra.Data.EntityConfig
{
    public class VeiculoConfiguration : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoConfiguration()
        {
            HasKey(v => v.Id);
            Property(v => v.Modelo)
                .IsRequired()
                .HasMaxLength(100);

            Property(v => v.AnoFabricacao)
                .IsRequired();

            Property(v => v.TipoDeVeiculo)
                .IsRequired();

            Property(v => v.Preco)
                .IsRequired();

            HasRequired(v => v.Fabricante)
                .WithMany(f => f.Veiculos) 
                .HasForeignKey(v => v.FabricanteVeiculoId)
                 .WillCascadeOnDelete(false);

            Property(v => v.Descricao)
                .HasMaxLength(500);
           
        }

    }
}
