using OnionSa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Models
{
    [Table("Produtos")]
    public class Produto : IProduto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Titulo { get; set; }
        [Required]    
        public int Preco { get; set; }

        ICollection<Pedido> Pedidos { get; set; }
    }
}
