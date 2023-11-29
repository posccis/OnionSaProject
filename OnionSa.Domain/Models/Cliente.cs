using OnionSa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Domain.Models
{
    public class Cliente : ICliente
    {
        [Key]
        [MaxLength(14)]
        public int CPFCNPJ { get; set; }
        [Required]
        [MaxLength(250)]
        public string RazaoSocial { get; set; }
        [Required]
        [MaxLength(8)]
        public int Cep { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
