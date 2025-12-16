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
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
           
            HasKey(c => c.Id);
            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                   .HasColumnAnnotation(
                     IndexAnnotation.AnnotationName,
                     new IndexAnnotation(new IndexAttribute("IX_CPF", 1) { IsUnique = true })
                 );

            Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(11);
        }
    }
}
