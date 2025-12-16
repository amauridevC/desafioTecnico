namespace VR.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarNomeESobrenomeAoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            AddColumn("dbo.AspNetUsers", "Sobrenome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Sobrenome");
            DropColumn("dbo.AspNetUsers", "Nome");
        }
    }
}
