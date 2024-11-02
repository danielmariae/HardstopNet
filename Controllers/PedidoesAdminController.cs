using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HardstopNet.Models;

namespace HardstopNet.Controllers
{
    public class PedidoesAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pedidoes
        public ActionResult Index()
        {
            var pedidoes = db.Pedidos.Include(p => p.Carrinho).Include(p => p.Pagamento).Include(p => p.User);
            return View(pedidoes.ToList());
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            ViewBag.CarrinhoId = new SelectList(db.Carrinhos, "CarrinhoId", "UserId");
            ViewBag.PagamentoId = new SelectList(db.Pagamentos, "PagamentoId", "FormaPagamento");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Pedidoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,HorarioPedido,StatusPedido,CarrinhoId,PagamentoId,UserId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarrinhoId = new SelectList(db.Carrinhos, "CarrinhoId", "UserId", pedido.CarrinhoId);
            ViewBag.PagamentoId = new SelectList(db.Pagamentos, "PagamentoId", "FormaPagamento", pedido.PagamentoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", pedido.UserId);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarrinhoId = new SelectList(db.Carrinhos, "CarrinhoId", "UserId", pedido.CarrinhoId);
            ViewBag.PagamentoId = new SelectList(db.Pagamentos, "PagamentoId", "FormaPagamento", pedido.PagamentoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", pedido.UserId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,HorarioPedido,StatusPedido,CarrinhoId,PagamentoId,UserId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarrinhoId = new SelectList(db.Carrinhos, "CarrinhoId", "UserId", pedido.CarrinhoId);
            ViewBag.PagamentoId = new SelectList(db.Pagamentos, "PagamentoId", "FormaPagamento", pedido.PagamentoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", pedido.UserId);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
