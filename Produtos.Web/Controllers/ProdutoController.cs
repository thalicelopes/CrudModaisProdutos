using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Produtos.Web.Data;
using Produtos.Web.Models;
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategoria");
            ViewBag.Produtos = _context.Produto.Include(x => x.Categoria).ToList();
            return View();
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
            return RedirectToAction("Index");
        }
    }
}
