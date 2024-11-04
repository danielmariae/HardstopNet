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
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(1000)]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "O preço deve ser positivo.")]
        public decimal Preco {  get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        public virtual ICollection<FavoritoProduto> FavoritoProdutos { get; set; } = new List<FavoritoProduto>();
        public virtual ICollection<ProdutoCategoria> ProdutosCategorias { get; set; } = new List<ProdutoCategoria>();
    }
}