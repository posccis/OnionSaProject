using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Repositories;
using OnionSa.Service.Exceptions;
using OnionSa.Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repo;
        private readonly OnionSaContext _cntxt;
        private readonly PedidoValidation PedidoValidation;
        public PedidoService(OnionSaContext cntxt)
        {
            _cntxt = cntxt;
            _repo = new PedidoRepository(_cntxt);
            PedidoValidation = new PedidoValidation();
        }

        /// <summary>
        /// Método que insere um pedido.
        /// </summary>
        /// <param name="pedido"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AdicionaPedido(Pedido pedido)
        {
            try
            {
                PedidoValidation.ValidaObjetoPedido(pedido);
                _repo.InserirPedido(pedido);
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
        /// Método que altera os dados de um pedido.
        /// </summary>
        /// <param name="pedido"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AlteraPedido(Pedido pedido)
        {
            try
            {
                PedidoValidation.ValidaObjetoPedido(pedido);
                _repo.AlterarPedido(pedido);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que remove um pedido através do seu numero.
        /// </summary>
        /// <param name="documento"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public async void RemovePedidoPorNumero(int numero)
        {
            try
            {
                var pedido = await _repo.ObterPedidoPorNumero(numero);

                PedidoValidation.ValidaObjetoPedido(pedido);

                _repo.RemoverPedido(pedido);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar remover o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método para obter um pedido através do seu numero.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns cref="Pedido">Retorna o objeto do pedido que possui o numero inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<Pedido> ObtemPedidoPorNumero(int numero)
        {
            try
            {
                var pedido = await _repo.ObterPedidoPorNumero(numero);

                PedidoValidation.ValidaObjetoPedido(pedido);

                return pedido;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método para obter a lista contentdo todos os pedidos.
        /// </summary>
        /// <returns cref="List{Pedido}">Retorna a lista contendo todos os pedidos.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<List<Pedido>> ObtemTodosOsPedidos()
        {
            try
            {
                var pedidos = await _repo.ObterTodosOsPedidos();

                PedidoValidation.ValidaListaDePedidos(pedidos);

                return pedidos;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os Pedidos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
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
