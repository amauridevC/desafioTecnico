using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Infra.Data.EntityConfig;
using VR.Infra.Data.EntityConfig.Convetions;

namespace VR.Infra.Data.Context
{
    public class VRContext : DbContext
    {
        public VRContext()  : base("VRContext")
        {
            Database.SetInitializer(new
            CreateDatabaseIfNotExists<VRContext>());

            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<FabricanteVeiculo> FabricanteVeiculos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SoftDelete", (ISoftDelete d) => d.IsDeleted, false);

            modelBuilder.Conventions.Add(new ConvencaoPrimaryKey());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new FabricanteVeiculoConfiguration());
            modelBuilder.Configurations.Add(new VeiculoConfiguration());
            modelBuilder.Configurations.Add(new VendaConfiguration());
            modelBuilder.Configurations.Add(new EnderecoConfiguration());

        }

        public int SaveChanges(string usuarioExclusao = "Sistema")
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DataExclusao = DateTime.Now;
                    entry.Entity.ExcluidoPor = usuarioExclusao;
                }
            }
            return base.SaveChanges();
        }

        public override int SaveChanges()
        {
            return SaveChanges("Sistema"); 
        }

    }
}
