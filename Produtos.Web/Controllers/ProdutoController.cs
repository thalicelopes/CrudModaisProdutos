using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produtos.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutosDbContext _context;
        public ProdutoController(ProdutosDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Produtos = _context.Produto.Include(x => x.Categoria).ToList();
            return View();
        }
    }
}
