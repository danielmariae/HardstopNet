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
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoController()
        {
            _context = new ApplicationDbContext();
        }

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

            return View(carrinhos); // Retorna uma lista para a view
        }


        // Adicionar um item ao carrinho
        [HttpPost]
        public ActionResult AdicionarAoCarrinho(int produtoId, int quantidade)
        {
            string usuarioId = User.Identity.GetUserId();
            var carrinho = _context.Carrinhos
                .Include("ItensCarrinho") // Corrigido: Usando o caminho como string
                .FirstOrDefault(c => c.UserId == usuarioId);

            if (carrinho == null)
            {
                carrinho = new Carrinho { UserId = usuarioId, DataCriacao = DateTime.Now };
                _context.Carrinhos.Add(carrinho);
            }

            var itemExistente = carrinho.ItensCarrinho.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (itemExistente != null)
            {
                itemExistente.QuantidadeProduto += quantidade;
            }
            else
            {
                carrinho.ItensCarrinho.Add(new ItemCarrinho
                {
                    ProdutoId = produtoId,
                    QuantidadeProduto = quantidade,
                    PrecoUnitario = _context.Produtos.Find(produtoId)?.Preco ?? 0
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Remover um item do carrinho
        [HttpPost]
        public ActionResult RemoverDoCarrinho(int itemCarrinhoId)
        {
            var itemCarrinho = _context.ItensCarrinho.Find(itemCarrinhoId);

            if (itemCarrinho != null)
            {
                _context.ItensCarrinho.Remove(itemCarrinho);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Aumentar a quantidade de um item no carrinho
        [HttpPost]
        public ActionResult AumentarQuantidade(int itemCarrinhoId)
        {
            var itemCarrinho = _context.ItensCarrinho.Find(itemCarrinhoId);

            if (itemCarrinho != null)
            {
                itemCarrinho.QuantidadeProduto += 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Diminuir a quantidade de um item no carrinho
        [HttpPost]
        public ActionResult DiminuirQuantidade(int? itemCarrinhoId)
        {
            if (!itemCarrinhoId.HasValue)
            {
                return RedirectToAction("Index");
            }

            var itemCarrinho = _context.ItensCarrinho.Find(itemCarrinhoId.Value);

            if (itemCarrinho != null)
            {
                if (itemCarrinho.QuantidadeProduto > 1)
                {
                    itemCarrinho.QuantidadeProduto -= 1;
                }
                else
                {
                    _context.ItensCarrinho.Remove(itemCarrinho);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        // GET: Carrinho/FinalizarCompra
        [Route("FinalizarCompra")]
        public ActionResult FinalizarCompra()
        {
            var usuarioId = User.Identity.GetUserId();
            var carrinho = _context.Carrinhos
                .Include("ItensCarrinho.Produto") // Correção: usando string para especificar o caminho de navegação
                .FirstOrDefault(c => c.User.Id == usuarioId);

            if (carrinho == null || !carrinho.ItensCarrinho.Any())
            {
                return RedirectToAction("Index", "Carrinho");
            }

            var pedido = new Pedido
            {
                User = _context.Users.Find(usuarioId),
                Carrinho = carrinho,
                HorarioPedido = DateTime.Now,
                StatusPedido = StatusPedido.Pendente,
                Pagamento = new Pagamento
                {
                    ValorPagamento = carrinho.ItensCarrinho.Sum(ic => ic.QuantidadeProduto * ic.PrecoUnitario),
                    FormaPagamento = "A definir",
                    ValidacaoPagamento = false
                }
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            return RedirectToAction("Detalhes", new { id = pedido.PedidoId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}