using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;

namespace VR.Infra.Data.EntityConfig
{
    public class FabricanteVeiculoConfiguration : EntityTypeConfiguration<FabricanteVeiculo>
    {
        public FabricanteVeiculoConfiguration()
        {
            HasKey(f => f.Id);
            Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_FabricanteVeiculo_Nome") { IsUnique = true }
                    )
                );

            Property(f => f.Pais)
                .IsRequired()
                .HasMaxLength(50);

            Property(f => f.Telefone)
                .IsRequired()
                .HasMaxLength(11);

            Property(f => f.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(f => f.AnoFundacao)
                .IsRequired();

            Property(f => f.Website)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(f => f.Veiculos)
                .WithRequired(v => v.Fabricante)
                .HasForeignKey(v => v.FabricanteVeiculoId)
                 .WillCascadeOnDelete(false);
        }
    }
}
