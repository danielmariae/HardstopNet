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
    public class CarrinhoesAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarrinhoesAdmin
        public ActionResult Index()
        {
            var carrinhoes = db.Carrinhos;
            return View(carrinhoes.ToList());
        }

        // GET: CarrinhoesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhos.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // GET: CarrinhoesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: CarrinhoesAdmin/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarrinhoId,UserId,DataCriacao")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.Carrinhos.Add(carrinho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", carrinho.UserId);
            return View(carrinho);
        }

        // GET: CarrinhoesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhos.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", carrinho.UserId);
            return View(carrinho);
        }

        // POST: CarrinhoesAdmin/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarrinhoId,UserId,DataCriacao")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrinho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", carrinho.UserId);
            return View(carrinho);
        }

        // GET: CarrinhoesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhos.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // POST: CarrinhoesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrinho carrinho = db.Carrinhos.Find(id);
            db.Carrinhos.Remove(carrinho);
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
