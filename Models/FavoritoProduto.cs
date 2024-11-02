using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class FavoritoProduto
    {
        [Key]
        public int FavoritoProdutoId { get; set; }

        public int FavoritosId { get; set; }
        public virtual Favoritos Favoritos { get; set; }

        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}