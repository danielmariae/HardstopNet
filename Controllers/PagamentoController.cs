using HardstopNet.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HardstopNet.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagamentoController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Pagamento
        public ActionResult Index()
        {
            var usuarioId = User.Identity.GetUserId();
            var carrinhos = _context.Carrinhos
                .Where(c => c.User.Id == usuarioId)
                .Include("ItensCarrinho.Produto") // Inclui os itens do carrinho e produtos
                .ToList(); // Transforma em lista

            // Calcula o subtotal somando o preço dos itens multiplicado pela quantidade
            ViewBag.Subtotal = carrinhos
                .SelectMany(c => c.ItensCarrinho)
                .Sum(item => item.PrecoUnitario * item.QuantidadeProduto);

            return View(carrinhos); // Passa a lista para a view
        }

            // GET: Pagamento/FinalizarCompra
            [Route("FinalizarCompra")]
            public ActionResult FinalizarCompra(String FormaPagamento)
            {
                var usuarioId = User.Identity.GetUserId();
                var carrinho = _context.Carrinhos
                    .Include("ItensCarrinho.Produto")
                    .FirstOrDefault(c => c.User.Id == usuarioId);

                if (carrinho == null || !carrinho.ItensCarrinho.Any())
                {
                    return RedirectToAction("Index", "Carrinho");
                }

                var horaPagamento = DateTime.Now;
                horaPagamento = new DateTime(horaPagamento.Year, horaPagamento.Month, horaPagamento.Day, horaPagamento.Hour, horaPagamento.Minute, horaPagamento.Second);


            var pedido = new Pedido
                {
                    User = _context.Users.Find(usuarioId),
                    Carrinho = carrinho,
                    HorarioPedido = horaPagamento,
                    StatusPedido = StatusPedido.Pendente,
                    Pagamento = new Pagamento
                    {
                        ValorPagamento = carrinho.ItensCarrinho.Sum(ic => ic.QuantidadeProduto * ic.PrecoUnitario),
                        FormaPagamento = FormaPagamento,
                        ValidacaoPagamento = false
                    }
                };

                _context.Pedidos.Add(pedido);
                _context.SaveChanges();

            return RedirectToAction("Details", "UserPedidos", new { id = pedido.PedidoId });
        }
    }
}
