namespace HardstopNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carrinhoes", "DataCriacao", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pedidoes", "HorarioPedido", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Pagamentoes", "DataHoraPagamento", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pagamentoes", "DataHoraPagamento", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pedidoes", "HorarioPedido", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.Carrinhoes", "DataCriacao", c => c.DateTime(nullable: false));
        }
    }
}
