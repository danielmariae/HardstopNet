using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardstopNet.Models
{
    public class Carrinho
    {
        [Key]
        public int CarrinhoId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime DataCriacao { get; set; }

        public virtual ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
    }
}