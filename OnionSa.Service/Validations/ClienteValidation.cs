using OnionSa.Domain.Models;
using OnionSa.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Validations
{
    public class ProdutoValidation
    {
        public void ValidaObjetoCliente(Cliente cliente) 
        {
            if (cliente == null) throw new OnionSaServiceException("O objeto está nulo ou vazio. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(cliente.RazaoSocial)) throw new OnionSaServiceException("É necessário informar a razão social do cliente. Revise os dados inseridos e tente novamente.");
            if (cliente.Cep <= 1 || cliente.Cep.ToString().Length < 8 || cliente.Cep.ToString().Length > 8) throw new OnionSaServiceException("É necessário informar um CEP válido. Revise os dados inseridos e tente novamente.");
            if (cliente.CPFCNPJ.ToString().Length < 11 || cliente.CPFCNPJ.ToString().Length < 14 || cliente.CPFCNPJ.ToString().Length > 14) throw new OnionSaServiceException("É necessário informar um CPF ou CNPJ válido. Revise os dados inseridos e tente novamente.");
        }

        public void ValidaListaDeClientes(List<Cliente> lista) 
        {

            if(lista.Count <= 0) throw new OnionSaServiceException("A lista está vazia e sem nenhum cliente. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
        }



    }
}
