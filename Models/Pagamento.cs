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
        public string FormaPagamento { get; set; }

        public DateTime DataHoraPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public bool ValidacaoPagamento {  get; set; }
    }
}