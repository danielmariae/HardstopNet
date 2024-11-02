namespace HardstopNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesOnFavoritos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtoes", "Favoritos_FavoritosId", "dbo.Favoritos");
            DropIndex("dbo.Produtoes", new[] { "Favoritos_FavoritosId" });
            CreateTable(
                "dbo.FavoritoProdutoes",
                c => new
                    {
                        FavoritoProdutoId = c.Int(nullable: false, identity: true),
                        FavoritosId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FavoritoProdutoId)
                .ForeignKey("dbo.Favoritos", t => t.FavoritosId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.FavoritosId)
                .Index(t => t.ProdutoId);
            
            DropColumn("dbo.Produtoes", "Favoritos_FavoritosId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produtoes", "Favoritos_FavoritosId", c => c.Int());
            DropForeignKey("dbo.FavoritoProdutoes", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.FavoritoProdutoes", "FavoritosId", "dbo.Favoritos");
            DropIndex("dbo.FavoritoProdutoes", new[] { "ProdutoId" });
            DropIndex("dbo.FavoritoProdutoes", new[] { "FavoritosId" });
            DropTable("dbo.FavoritoProdutoes");
            CreateIndex("dbo.Produtoes", "Favoritos_FavoritosId");
            AddForeignKey("dbo.Produtoes", "Favoritos_FavoritosId", "dbo.Favoritos", "FavoritosId");
        }
    }
}
