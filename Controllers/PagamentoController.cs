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

            // Validar se o estoque é suficiente para cada item
            foreach (var item in carrinho.ItensCarrinho)
            {
                var produto = item.Produto;
                if (produto == null || produto.Estoque < item.QuantidadeProduto)
                {
                    ModelState.AddModelError("", $"Estoque insuficiente para o produto {produto?.Nome ?? "desconhecido"}.");
                    return View("Index", carrinho);
                }
            }

            var horaPagamento = DateTime.Now;

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
                    ValidacaoPagamento = false,
                    DataHoraPagamento = DateTime.Now
                }
            };

            // Reduzir o estoque de cada produto
            foreach (var item in carrinho.ItensCarrinho)
            {
                var produto = item.Produto;
                if (produto != null)
                {
                    produto.Estoque -= item.QuantidadeProduto;
                }
            }

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            return RedirectToAction("Details", "UserPedidos", new { id = pedido.PedidoId });
        }

    }
}
