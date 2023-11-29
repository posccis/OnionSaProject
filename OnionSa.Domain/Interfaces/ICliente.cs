using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Interfaces
{
    public interface ICliente
    {
        public int CPFCNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public int Cep { get; set; }
    }
}
