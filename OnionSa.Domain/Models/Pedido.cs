﻿using OnionSa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Models
{
    [Table("Pedidos")]
    public class Pedido : IPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumeroDoPedido { get; set; }

        [Required]
        public string CPFCNPJ { get; set; }

        [ForeignKey("CPFCNPJ")]
        public Cliente Cliente { get; set; }

        [Required]
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        [Required]
        [MaxLength(8)]
        public string Cep { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
