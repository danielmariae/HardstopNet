using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class Favoritos
    {
        [Key]
        public int FavoritosId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<FavoritoProduto> FavoritoProdutos { get; set; } = new List<FavoritoProduto>();
    }
}