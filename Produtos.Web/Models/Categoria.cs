using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Web.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, informe o nome da categoria. ")]
        public string NomeCategoria { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
