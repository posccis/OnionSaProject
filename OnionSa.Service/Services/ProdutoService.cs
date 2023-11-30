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
    public class ProdutoService
    {
        private readonly ProdutoRepository _repo;
        private readonly OnionSaContext _cntxt;
        private readonly ProdutoValidation ProdutoValidation;
        public ProdutoService(OnionSaContext cntxt)
        {
            _cntxt = cntxt;
            _repo = new ProdutoRepository(_cntxt);
            ProdutoValidation = new ProdutoValidation();
        }

        /// <summary>
        /// Método que insere um cliente.
        /// </summary>
        /// <param name="produto"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AdicionaProduto(Produto produto)
        {
            try
            {
                ProdutoValidation.ValidaObjetoProduto(produto);
                _repo.InserirProduto(produto);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que altera os dados de um cliente.
        /// </summary>
        /// <param name="produto"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AlteraProduto(Produto produto)
        {
            try
            {
                ProdutoValidation.ValidaObjetoProduto(produto);
                _repo.AlterarProduto(produto);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método que remove um cliente através do seu CNPJ ou CPF.
        /// </summary>
        /// <param name="documento"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public async void RemoveProdutoPorId(int id)
        {
            try
            {
                var produto = await _repo.ObterProdutoPorId(id);

                ProdutoValidation.ValidaObjetoProduto(produto);

                _repo.RemoverProduto(produto);

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
        /// Método para obter um produto através do seu Id.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns cref="Produto">Retorna o objeto do produto que possui o Id inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<Produto> ObtemProdutoPorId(int id)
        {
            try
            {
                var produto = await _repo.ObterProdutoPorId(id);

                ProdutoValidation.ValidaObjetoProduto(produto);

                return produto;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }

        /// <summary>
        /// Método para obter a lista contentdo todos os produtos.
        /// </summary>
        /// <returns cref="List{Produto}">Retorna a lista contendo todos os produtos.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<List<Produto>> ObtemTodosOsProdutos()
        {
            try
            {
                var produtos = await _repo.ObterTodosOsProdutos();

                ProdutoValidation.ValidaListaDeProdutos(produtos);

                return produtos;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os Produtos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
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
