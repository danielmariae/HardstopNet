namespace HardstopNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrinhoes",
                c => new
                    {
                        CarrinhoId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CarrinhoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ItemCarrinhoes",
                c => new
                    {
                        ItemCarrinhoId = c.Int(nullable: false, identity: true),
                        QuantidadeProduto = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarrinhoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCarrinhoId)
                .ForeignKey("dbo.Carrinhoes", t => t.CarrinhoId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.CarrinhoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estoque = c.Int(nullable: false),
                        Favoritos_FavoritosId = c.Int(),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Favoritos", t => t.Favoritos_FavoritosId)
                .Index(t => t.Favoritos_FavoritosId);
            
            CreateTable(
                "dbo.ProdutoCategorias",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.CategoriaId })
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        HorarioPedido = c.DateTime(nullable: false),
                        StatusPedido = c.Int(nullable: false),
                        CarrinhoId = c.Int(nullable: false),
                        PagamentoId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Carrinhoes", t => t.CarrinhoId, cascadeDelete: true)
                .ForeignKey("dbo.Pagamentoes", t => t.PagamentoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CarrinhoId)
                .Index(t => t.PagamentoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pagamentoes",
                c => new
                    {
                        PagamentoId = c.Int(nullable: false, identity: true),
                        FormaPagamento = c.String(),
                        DataHoraPagamento = c.DateTime(nullable: false),
                        ValorPagamento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValidacaoPagamento = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PagamentoId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Favoritos",
                c => new
                    {
                        FavoritosId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FavoritosId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Favoritos", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Produtoes", "Favoritos_FavoritosId", "dbo.Favoritos");
            DropForeignKey("dbo.Carrinhoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pedidoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pedidoes", "PagamentoId", "dbo.Pagamentoes");
            DropForeignKey("dbo.Pedidoes", "CarrinhoId", "dbo.Carrinhoes");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ItemCarrinhoes", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.ProdutoCategorias", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.ProdutoCategorias", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.ItemCarrinhoes", "CarrinhoId", "dbo.Carrinhoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Favoritos", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Pedidoes", new[] { "UserId" });
            DropIndex("dbo.Pedidoes", new[] { "PagamentoId" });
            DropIndex("dbo.Pedidoes", new[] { "CarrinhoId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProdutoCategorias", new[] { "CategoriaId" });
            DropIndex("dbo.ProdutoCategorias", new[] { "ProdutoId" });
            DropIndex("dbo.Produtoes", new[] { "Favoritos_FavoritosId" });
            DropIndex("dbo.ItemCarrinhoes", new[] { "ProdutoId" });
            DropIndex("dbo.ItemCarrinhoes", new[] { "CarrinhoId" });
            DropIndex("dbo.Carrinhoes", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Favoritos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Pagamentoes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categorias");
            DropTable("dbo.ProdutoCategorias");
            DropTable("dbo.Produtoes");
            DropTable("dbo.ItemCarrinhoes");
            DropTable("dbo.Carrinhoes");
        }
    }
}
