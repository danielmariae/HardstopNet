using System.Linq;
using System.Web.Mvc;
using HardstopNet.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace HardstopNet.Controllers
{
   public class UserPedidosController : Controller
    {
        private ApplicationDbContext _context;

        public UserPedidosController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: UserPedidos
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var pedidos = _context.Pedidos
                .Include(p => p.Carrinho.ItensCarrinho.Select(i => i.Produto))
                .Include(p => p.Pagamento)
                .Where(p => p.UserId == userId)
                .ToList();

            return View(pedidos);
        }

        // GET: UserPedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var pedido = _context.Pedidos
                .Include(p => p.Carrinho.ItensCarrinho.Select(i => i.Produto))
                .Include(p => p.Pagamento)
                .SingleOrDefault(p => p.PedidoId == id && p.UserId == userId);

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
