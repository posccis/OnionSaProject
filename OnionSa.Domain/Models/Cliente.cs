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
    public class Cliente : ICliente
    {
        [Key]
        [MaxLength(14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CPFCNPJ { get; set; }

        [Required]
        [MaxLength(250)]
        public string RazaoSocial { get; set; }


        public ICollection<Pedido> Pedidos { get; set; }
    }
}
