using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Interfaces
{
    public interface IProduto
    {
        public int ProdutoId { get; set; }
        public string Titulo { get; set; }
        public int Preco { get; set; }
    }
}
