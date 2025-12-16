namespace VR.Infra.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class inicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enderecos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rua = c.String(nullable: false, maxLength: 90),
                        Cidade = c.String(nullable: false, maxLength: 36),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Cep = c.String(nullable: false, maxLength: 8),
                        FabricanteVeiculoId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Endereco_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FabricanteVeiculo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Pais = c.String(nullable: false, maxLength: 50),
                        EnderecoId = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false, maxLength: 100),
                        AnoFundacao = c.DateTime(nullable: false),
                        Website = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FabricanteVeiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enderecos", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Nome, unique: true, name: "IX_FabricanteVeiculo_Nome");
            
            CreateTable(
                "dbo.Veiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modelo = c.String(nullable: false, maxLength: 100),
                        AnoFabricacao = c.DateTime(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoDeVeiculo = c.Int(nullable: false),
                        Descricao = c.String(maxLength: 500),
                        FabricanteVeiculoId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Veiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FabricanteVeiculo", t => t.FabricanteVeiculoId)
                .Index(t => t.FabricanteVeiculoId);
            
            CreateTable(
                "dbo.Venda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeiculoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataVenda = c.DateTime(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Protocolo = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Veiculo", t => t.VeiculoId)
                .Index(t => t.VeiculoId)
                .Index(t => t.ClienteId)
                .Index(t => t.Protocolo, unique: true, name: "IX_Venda_Protocolo");
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Cpf = c.String(nullable: false, maxLength: 11),
                        Telefone = c.String(nullable: false, maxLength: 11),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cliente_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Cpf, unique: true, name: "IX_CPF");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venda", "VeiculoId", "dbo.Veiculo");
            DropForeignKey("dbo.Venda", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.FabricanteVeiculo", "Id", "dbo.Enderecos");
            DropForeignKey("dbo.Veiculo", "FabricanteVeiculoId", "dbo.FabricanteVeiculo");
            DropIndex("dbo.Cliente", "IX_CPF");
            DropIndex("dbo.Venda", "IX_Venda_Protocolo");
            DropIndex("dbo.Venda", new[] { "ClienteId" });
            DropIndex("dbo.Venda", new[] { "VeiculoId" });
            DropIndex("dbo.Veiculo", new[] { "FabricanteVeiculoId" });
            DropIndex("dbo.FabricanteVeiculo", "IX_FabricanteVeiculo_Nome");
            DropIndex("dbo.FabricanteVeiculo", new[] { "Id" });
            DropTable("dbo.Cliente",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cliente_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Venda");
            DropTable("dbo.Veiculo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Veiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FabricanteVeiculo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FabricanteVeiculo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Enderecos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Endereco_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
