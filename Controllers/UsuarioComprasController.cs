using System.Linq;
using System.Web.Mvc;
using HardstopNet.Models; 
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System;

namespace HardstopNet.Controllers
{
    [Authorize(Roles = "Cliente")]
    [RoutePrefix("UsuarioCompras")]
    public class UsuarioComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioComprasController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: UsuarioCompras/Pedidos/5
        public ActionResult Detalhes(int id)
        {
            var usuarioId = User.Identity.GetUserId();
            var pedido = _context.Pedidos
                .Where(p => p.PedidoId == id && p.User.Id == usuarioId)
                .Include(p => p.Carrinho)
                .Include(p => p.Carrinho.ItensCarrinho.Select(ic => ic.Produto))
                .Include(p => p.Pagamento)
                .FirstOrDefault();

            if (pedido == null)
            {
                return HttpNotFound();
            }

            return View(pedido);
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
