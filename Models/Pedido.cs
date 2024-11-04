using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        public DateTime HorarioPedido { get; set; }
        
        [Required]
        public StatusPedido StatusPedido { get; set; }

        [ForeignKey("Carrinho")]
        public int CarrinhoId { get; set; }
        public virtual Carrinho Carrinho { get; set; }

        [ForeignKey("Pagamento")]
        public int PagamentoId { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
    public enum StatusPedido
    {
        Pendente = 0,
        Processando = 1,
        Enviado = 2,
        Concluido = 3,
        Cancelado = 4
    }
}