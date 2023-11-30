using OnionSa.Domain.Models;
using OnionSa.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Validations
{
    public class PedidoValidation
    {
        public void ValidaObjetoPedido(Pedido pedido) 
        {
            if (pedido == null) throw new OnionSaServiceException("O objeto está nulo ou vazio. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(pedido.)) throw new OnionSaServiceException("É necessário informar a razão social do pedido. Revise os dados inseridos e tente novamente.");
            if (pedido.Cep <= 1 || pedido.Cep.ToString().Length < 8 || pedido.Cep.ToString().Length > 8) throw new OnionSaServiceException("É necessário informar um CEP válido. Revise os dados inseridos e tente novamente.");
            if (pedido.CPFCNPJ.ToString().Length < 11 || pedido.CPFCNPJ.ToString().Length < 14 || pedido.CPFCNPJ.ToString().Length > 14) throw new OnionSaServiceException("É necessário informar um CPF ou CNPJ válido. Revise os dados inseridos e tente novamente.");
        }

        public void ValidaListaDePedidos(List<Pedido> lista) 
        {

            if(lista.Count <= 0) throw new OnionSaServiceException("A lista está vazia e sem nenhum pedido. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
        }



    }
}
