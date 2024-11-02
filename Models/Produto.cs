using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        
        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public decimal Preco {  get; set; }
        
        [Required]
        public int Estoque { get; set; }

        public virtual ICollection<FavoritoProduto> FavoritoProdutos { get; set; } = new List<FavoritoProduto>();
        public virtual ICollection<ProdutoCategoria> ProdutosCategorias { get; set; } = new List<ProdutoCategoria>();
    }
}