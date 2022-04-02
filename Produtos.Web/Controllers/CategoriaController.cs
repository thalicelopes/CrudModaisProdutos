using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produtos.Web.Data;
using Produtos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ProdutosDbContext _context;
        public CategoriaController(ProdutosDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Categorias = _context.Categoria.Include(x => x.Produto).ToList();
            return View();
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
        public async Task<IActionResult> DeletarProduto(int? id)
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
        public async Task<IActionResult> EditarCategoria([Bind("Id, NomeCategoria")] Categoria Categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Categoria);
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
    }
}
