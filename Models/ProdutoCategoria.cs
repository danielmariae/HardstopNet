using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class ProdutoCategoria
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }


    }
}