using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Exceptions;
using OnionSa.Repository.Repositories;

namespace OnionSa.Test
{
    public class RepositoryTest
    {

        #region Testes de Produtos
        [Fact]
        public void DeveInserirUmProdutoAoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ProdutoRepository(cntxt);

            Produto produto = new Produto()
            {
                
                Preco = 1000,
                Titulo = "Celular"
            };

            repo.InserirProduto(produto);

            var resultado = repo.ObterProdutoPorId(1);
            Assert.NotNull(resultado);
        }
        [Fact]
        public async void DeveRetornarUmProdutoDoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ProdutoRepository(cntxt);

            var resultado = await repo.ObterProdutoPorId(1);
            Assert.NotNull(resultado);
        }
        [Fact]
        public void DeveAtualizarUmProdutoNoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ProdutoRepository(cntxt);

            Produto produto = new Produto()
            {
                ProdutoId = 1,
                Preco = 1000,
                Titulo = "Celulares"
            };

            repo.AlterarProduto(produto);

            var resultado = repo.ObterProdutoPorId(1).Result;
            Assert.Equal<Produto>(produto, resultado);            
        }
        [Fact]
        public async void DeveExcluirUmProduto()
        {
            var cntxt = new OnionSaContext();
            var repo = new ProdutoRepository(cntxt);

            var produto = await repo.ObterProdutoPorId(1);
            repo.RemoverProduto(produto);

            var resultado = await repo.ObterProdutoPorId(1);

            Assert.Null(resultado);
        }
        #endregion Testes de produtos

        #region Testes de Clientes
        [Fact]
        public void DeveInserirUmClienteAoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ClienteRepository(cntxt);

            Cliente cliente = new Cliente()
            {
                CPFCNPJ = 12229716409,
                RazaoSocial = "Victor Hugo de Oliveira Gomes",
                Cep = 54768790,
            };

            repo.InserirCliente(cliente);

            var resultado = repo.ObterClientePorDoc(12229716409);
            Assert.NotNull(resultado);
        }
        [Fact]
        public async void DeveRetornarUmClienteDoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ClienteRepository(cntxt);

            var resultado = await repo.ObterClientePorDoc(12229716409);
            Assert.NotNull(resultado);
        }
        [Fact]
        public async void DeveRetornarTodosOsClientesDoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ClienteRepository(cntxt);

            var resultado = await repo.ObterTodosOsClientes();
            Assert.True(resultado.Count > 0);
        }
        [Fact]
        public void DeveAtualizarUmClienteNoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new ClienteRepository(cntxt);

            Cliente cliente = new Cliente()
            {
                CPFCNPJ = 12229716409,
                RazaoSocial = "Victor Hugo de Oliveira Gomes",
                Cep = 54768799,
            };


            repo.AlterarCliente(cliente);

            var resultado = repo.ObterClientePorDoc(12229716409).Result;
            Assert.Equal<Cliente>(cliente, resultado);
        }

        [Fact]
        public void DeveExcluirUmCliente()
        {
            var cntxt = new OnionSaContext();
            var repo = new ClienteRepository(cntxt);

            var cliente = repo.ObterClientePorDoc(12229716409).Result;
            repo.RemoverCliente(cliente);

            var resultado = repo.ObterClientePorDoc(12229716409).Result;

            Assert.Null(resultado);
        }
        #endregion Testes de Clientes

        #region Testes de Pedidos
        [Fact]
        public void DeveInserirUmPedidoAoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new PedidoRepository(cntxt);

            Pedido pedido = new Pedido()
            {
                CPFCNPJ = 12229716409,
                ProdutoId = 2,
                Data = DateTime.Today
            };

            repo.InserirPedido(pedido);

            var resultado = repo.ObterPedidoPorNumero(1);
            Assert.NotNull(resultado);
        }
        [Fact]
        public async void DeveRetornarUmPedidoDoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new PedidoRepository(cntxt);

            var resultado = await repo.ObterPedidoPorNumero(1);
            Assert.NotNull(resultado);
        }
        [Fact]
        public async void DeveRetornarTodosOsPedidosDoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new PedidoRepository(cntxt);

            var resultado = await repo.ObterTodosOsPedidos();
            Assert.True(resultado.Count > 0);
        }
        [Fact]
        public void DeveAtualizarUmPedidoNoBanco()
        {
            var cntxt = new OnionSaContext();
            var repo = new PedidoRepository(cntxt);

            Pedido pedido = new Pedido()
            {
                CPFCNPJ = 12229716409,
                ProdutoId = 3,
                Data = DateTime.Parse("10/12/2023"),
            };


            repo.AlterarPedido(pedido);

            var resultado = repo.ObterPedidoPorNumero(1).Result;
            Assert.Equal<Pedido>(pedido, resultado);
        }

        [Fact]
        public void DeveExcluirUmPedido()
        {
            var cntxt = new OnionSaContext();
            var repo = new PedidoRepository(cntxt);

            var pedido = repo.ObterPedidoPorNumero(1).Result;
            repo.RemoverPedido(pedido);

            var resultado = repo.ObterPedidoPorNumero(1).Result;

            Assert.Null(resultado);
        }
        #endregion Testes de Clientes

    }
}