namespace VR.Infra.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarSoftDelete : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.Venda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeiculoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataVenda = c.DateTime(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Protocolo = c.String(maxLength: 40),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Venda_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.Venda", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Venda", "DataExclusao", c => c.DateTime());
            AddColumn("dbo.Venda", "ExcluidoPor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venda", "ExcluidoPor");
            DropColumn("dbo.Venda", "DataExclusao");
            DropColumn("dbo.Venda", "IsDeleted");
            AlterTableAnnotations(
                "dbo.Venda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeiculoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataVenda = c.DateTime(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Protocolo = c.String(maxLength: 40),
                        IsDeleted = c.Boolean(nullable: false),
                        DataExclusao = c.DateTime(),
                        ExcluidoPor = c.String(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Venda_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
