using Microsoft.EntityFrameworkCore;
using Produtos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Data
{
    public partial class ProdutosDbContext : DbContext
    {
        public ProdutosDbContext(DbContextOptions<ProdutosDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
    }
}
