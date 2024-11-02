using HardstopNet.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HardstopNet.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritosController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Favoritos 
        public ActionResult Index()
        {
            var usuarioId = User.Identity.GetUserId();
            var favoritos = _context.Favoritos
                .Where(f => f.UserId == usuarioId)
                .SelectMany(f => f.FavoritoProdutos.Select(fp => fp.Produto))
                .ToList();

            return View(favoritos); // Retorna uma lista para a view
        }

        [HttpPost]
        public ActionResult AdicionarFavorito(int produtoId)
        {
            var userId = User.Identity.GetUserId();

            // Busca o objeto Favoritos do usuário, ou cria um novo se não existir
            var favoritos = _context.Favoritos.FirstOrDefault(f => f.UserId == userId);
            if (favoritos == null)
            {
                favoritos = new Favoritos { UserId = userId };
                _context.Favoritos.Add(favoritos);
            }

            // Verifica se o produto já está nos favoritos do usuário
            var jaFavorito = favoritos.FavoritoProdutos.Any(fp => fp.ProdutoId == produtoId);
            if (!jaFavorito)
            {
                // Adiciona o produto aos favoritos
                favoritos.FavoritoProdutos.Add(new FavoritoProduto
                {
                    ProdutoId = produtoId,
                    FavoritosId = favoritos.FavoritosId
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoverFavorito(int produtoId)
        {
            var userId = User.Identity.GetUserId();

            // Busca o objeto Favoritos do usuário
            var favoritos = _context.Favoritos.FirstOrDefault(f => f.UserId == userId);
            if (favoritos == null)
            {
                return HttpNotFound("Favoritos não encontrado para o usuário.");
            }

            // Busca a relação FavoritoProduto específica para este produto
            var favoritoProduto = favoritos.FavoritoProdutos.FirstOrDefault(fp => fp.ProdutoId == produtoId);
            if (favoritoProduto != null)
            {
                favoritos.FavoritoProdutos.Remove(favoritoProduto);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}