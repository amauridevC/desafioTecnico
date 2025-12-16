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
    public class VendaConfiguration : EntityTypeConfiguration<Venda>
    {
        public VendaConfiguration()
        {
            HasKey(v => v.Id);
            Property(v => v.DataVenda)
                .IsRequired();

            Property(v => v.PrecoVenda)
                .IsRequired();

            HasRequired(v => v.Veiculo)
                .WithMany()
                .HasForeignKey(v => v.VeiculoId)
                 .WillCascadeOnDelete(false);


            Property(v => v.Protocolo)
                .HasMaxLength(40)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Venda_Protocolo") { IsUnique = true })); ;
        }
    }
}
