using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Produtos.Web.Data;
using Produtos.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutosDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProdutosDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategoria");
            ViewBag.Produtos = _context.Produto.Include(x => x.Categoria).ToList();
            ViewBag.Categorias = _context.Categoria.Include(x => x.Produto).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CriarProduto(int id, [Bind("Id,NomeProduto,QtdeProduto,MarcaProduto,PrecoProduto,CategoriaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }
        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
        public async Task<IActionResult> DeletarProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Produto = await _context.Produto.FindAsync(id);
            _context.Remove(Produto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditarProduto([Bind("Id,NomeProduto,QtdeProduto,MarcaProduto,PrecoProduto,CategoriaId")] Produto produto)
        {
            if (produto.CategoriaId == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var prod = _context.Produto.Where(x => x.Id == produto.Id).FirstOrDefault();
                        prod.NomeProduto = produto.NomeProduto;
                        prod.QtdeProduto = produto.QtdeProduto;
                        prod.MarcaProduto = produto.MarcaProduto;
                        prod.PrecoProduto = produto.PrecoProduto;
                        _context.Update(prod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(produto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
        public async Task<IActionResult> CriarCategoria([Bind("Id, NomeCategoria")] Categoria Categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(Categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(Categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletarCategoriaProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Produtos = _context.Produto.Where(x => x.CategoriaId == id).ToList();
            _context.Produto.RemoveRange(Produtos);
            var Categoria = await _context.Categoria.FindAsync(id);
            _context.Remove(Categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditarCategoria([Bind("Categoria")] Produto produtoCategoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaEditada = _context.Categoria.Where(x => x.Id == produtoCategoria.Categoria.Id).FirstOrDefault();
                    categoriaEditada.NomeCategoria = produtoCategoria.Categoria.NomeCategoria;
                    _context.Update(categoriaEditada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(produtoCategoria.Categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
