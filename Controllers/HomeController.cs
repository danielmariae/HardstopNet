using HardstopNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HardstopNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            
            var produtos = _context.Produtos.ToList(); // Recupera todos os produtos
            return View(produtos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}