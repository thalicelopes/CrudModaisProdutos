using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int QtdeProduto { get; set; }
        public string MarcaProduto { get; set; }
        public double PrecoProduto { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria Categoria { get; set; }
    }
}
