using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        public String Nome { get; set; }

        public virtual ICollection<ProdutoCategoria> ProdutosCategorias { get; set; } = new List<ProdutoCategoria>();

    }
}