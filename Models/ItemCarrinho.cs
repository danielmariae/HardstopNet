using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class ItemCarrinho
    {
        [Key]
        public int ItemCarrinhoId { get; set; }

        [Required]
        // [Range]
        public int QuantidadeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }

        [ForeignKey("Carrinho")]
        public int CarrinhoId { get; set; }
        public virtual Carrinho Carrinho { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}