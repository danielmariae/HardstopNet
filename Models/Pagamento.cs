using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardstopNet.Models
{
    public class Pagamento
    {
        [Key]
        public int PagamentoId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FormaPagamento { get; set; }

        [Required]
        public DateTime DataHoraPagamento { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "O valor do pagamento deve ser positivo.")]
        public decimal ValorPagamento { get; set; }

        public bool ValidacaoPagamento {  get; set; }
    }
}