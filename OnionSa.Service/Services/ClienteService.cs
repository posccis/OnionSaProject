using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Interfaces;
using OnionSa.Repository.Repositories;
using OnionSa.Service.Exceptions;
using OnionSa.Service.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Services
{
    public class ClienteService
    {
		private readonly IClienteRepository _repo;
        private readonly OnionSaContext _cntxt;
        private readonly ClienteValidation clienteValidation;
        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
            clienteValidation = new ClienteValidation();
        }

        /// <summary>
        /// Método que constrói um objeto Cliente através de um DataRow.
        /// </summary>
        /// <param name="linha"></param>
        /// <returns cref="Cliente">Retorna o objeto cliente extraido DataRow inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public Cliente CriaObjetoCliente(DataRow linha)
        {
            try
            {
                Cliente cliente = new Cliente()
                {
                    CPFCNPJ = linha[0].ToString(),
                    RazaoSocial = linha[1].ToString()
                    
                };

                return cliente;
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que insere um cliente.
        /// </summary>
        /// <param name="cliente"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AdicionaCliente(Cliente cliente)
        {
            try
            {
                clienteValidation.ValidaObjetoCliente(cliente);
                _repo.InserirCliente(cliente);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que insere vários clientes.
        /// </summary>
        /// <param name="clientes"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AdicionaVariosClientes(List<Cliente> clientes)
        {
            try
            {
                clienteValidation.ValidaListaClientes(clientes);
                _repo.InserirVariosClientes(clientes);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir a lista de clientes. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que altera os dados de um cliente.
        /// </summary>
        /// <param name="cliente"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AlteraCliente(Cliente cliente)
        {
            try
            {
                clienteValidation.ValidaObjetoCliente(cliente);
                _repo.AlterarCliente(cliente);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que remove um cliente através do seu CNPJ ou CPF.
        /// </summary>
        /// <param name="documento"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public async void RemoveClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _repo.ObterClientePorDoc(documento);

                clienteValidation.ValidaObjetoCliente(cliente);

                _repo.RemoverCliente(cliente);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar remover o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método para obter um cliente através do seu CPF ou CNPJ.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns cref="Cliente">Retorna o objeto do cliente que possui o documento inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<Cliente> ObtemClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _repo.ObterClientePorDoc(documento);

                clienteValidation.ValidaObjetoCliente(cliente);

                return cliente;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método para obter a lista contentdo todos os clientes.
        /// </summary>
        /// <returns cref="List{Cliente}">Retorna a lista contendo todos os clientes.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<List<Cliente>> ObtemTodosOsClientes() 
        {
            try
            {
                var clientes = await _repo.ObterTodosOsClientes();

                clienteValidation.ValidaListaDeClientes(clientes);

                return clientes;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os clientes. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        //public async Task<List<Cliente>> ObtemTodosOsPedidosPorCliente(Cliente cliente) 
        //{
        //    try
        //    {
        //        var clientes = await _repo.ObterTodosOsClientes();
        //        var clienteResultado = clientes.FirstOrDefault(cl => cl.CPFCNPJ == cliente.CPFCNPJ);

        //        clienteResultado.Pedidos;

        //        clienteValidation.ValidaListaDeClientes(clientes);

        //        return clientes;

        //    }
        //    catch (OnionSaServiceException onionExcp)
        //    {
        //        throw onionExcp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os clientes. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
        //    }
        //}
    }
}
