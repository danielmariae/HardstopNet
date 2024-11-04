namespace HardstopNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesA1FinalDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produtoes", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Produtoes", "Descricao", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Pagamentoes", "FormaPagamento", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pagamentoes", "FormaPagamento", c => c.String());
            AlterColumn("dbo.Produtoes", "Descricao", c => c.String());
            AlterColumn("dbo.Produtoes", "Nome", c => c.String(nullable: false));
        }
    }
}
