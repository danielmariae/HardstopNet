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
    [Authorize(Roles = "Funcionario")]
    public class ProdutoesAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProdutoesAdmin
        public ActionResult Index()
        {
            var produtos = db.Produtos.Include(p => p.ProdutosCategorias.Select(pc => pc.Categoria)).ToList();
            return View(db.Produtos.ToList());
        }

        // GET: ProdutoesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Include(p => p.ProdutosCategorias.Select(pc => pc.Categoria))
                                         .SingleOrDefault(p => p.ProdutoId == id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: ProdutoesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaIds = new MultiSelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: ProdutoesAdmin/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,Nome,Descricao,Preco,Estoque")] Produto produto, int[] CategoriaIds)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();

                // Adiciona as categorias selecionadas
                if (CategoriaIds != null)
                {
                    foreach (var categoriaId in CategoriaIds)
                    {
                        db.ProdutoCategorias.Add(new ProdutoCategoria
                        {
                            ProdutoId = produto.ProdutoId,
                            CategoriaId = categoriaId
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.CategoriaIds = new MultiSelectList(db.Categorias, "CategoriaId", "Nome");
            return View(produto);
        }

        // GET: ProdutoesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Include(p => p.ProdutosCategorias.Select(pc => pc.Categoria))
                                         .SingleOrDefault(p => p.ProdutoId == id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaIds = new MultiSelectList(db.Categorias, "CategoriaId", "Nome", produto.ProdutosCategorias.Select(c => c.CategoriaId));
            return View(produto);
        }

        // POST: ProdutoesAdmin/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Descricao,Preco,Estoque")] Produto produto, int[] CategoriaIds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();

                // Remover categorias antigas
                var existingCategories = db.ProdutoCategorias.Where(pc => pc.ProdutoId == produto.ProdutoId).ToList();
                foreach (var categoria in existingCategories)
                {
                    db.ProdutoCategorias.Remove(categoria);
                }

                // Adiciona as categorias selecionadas
                if (CategoriaIds != null)
                {
                    foreach (var categoriaId in CategoriaIds)
                    {
                        db.ProdutoCategorias.Add(new ProdutoCategoria
                        {
                            ProdutoId = produto.ProdutoId,
                            CategoriaId = categoriaId
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaIds = new MultiSelectList(db.Categorias, "CategoriaId", "Nome", CategoriaIds);
            return View(produto);
        }

        // GET: ProdutoesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: ProdutoesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
