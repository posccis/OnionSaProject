﻿using OnionSa.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Interfaces
{
    public interface IPedido
    {
        public int NumeroDoPedido { get; set; }
        public string CPFCNPJ { get; set; }
        public Cliente Cliente { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public string Cep { get; set; }
        public DateTime Data { get; set; }
    }
}
