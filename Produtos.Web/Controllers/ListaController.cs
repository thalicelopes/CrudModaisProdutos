using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Produtos.Web.Data;
using Produtos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Controllers
{
    public class ListaController : Controller
    {
        private readonly ProdutosDbContext _context;
        private readonly ILogger<ListaController> _logger;

        public ListaController(ILogger<ListaController> logger, ProdutosDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Produto> prod = new List<Produto>();
            prod = GetCliente();
            var lala = 1;
            return View();
        }
        public IEnumerable<Produto> GetCliente()
        {
            IList<Produto> produtoLista = new List<Produto>();
            var consulta = from q in _context.Produto
                           select q;
            try
            {
                var produto = consulta.ToList();
                foreach (var produtoDados in produto)
                {
                    produtoLista.Add(new Produto()
                    {
                        NomeProduto = produtoDados.NomeProduto,
                        MarcaProduto = produtoDados.MarcaProduto,
                        PrecoProduto = produtoDados.PrecoProduto,
                        QtdeProduto = produtoDados.QtdeProduto,
                    });
                }
                return produtoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
