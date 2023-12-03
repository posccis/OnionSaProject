using Microsoft.AspNetCore.Mvc;
using OnionSa.Domain.Models;
using OnionSa.Repository.Interfaces;
using OnionSa.Service.Exceptions;
using OnionSa.Service.Services;
using OnionSa.Service.Validations;
using System.Data;

namespace OnionSA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _repo;
        private readonly PedidoService _service;
        private readonly ClienteService _clienteService;
        private readonly ProdutoService _produtoService;
        public PedidosController(IPedidoRepository repo, IClienteRepository clienteRepo, IProdutoRepository produtoRepo)
        {
            _repo = repo;
            _service = new PedidoService(repo);
            _clienteService = new ClienteService(clienteRepo);
            _produtoService = new ProdutoService(produtoRepo);

        }

        [AcceptVerbs("POST"), Route("onionsa/enviarplanilha")]
        [HttpPost]
        public async Task<IActionResult> EnviarPlanilha([FromForm]IFormFile planilha)
        {
            CSVService csvService = new CSVService();
            CSVValidation csvValidation = new CSVValidation();
            PedidoValidation pedidoValidation = new PedidoValidation();
            ClienteValidation clienteValidation = new ClienteValidation();
            List<Cliente> clientes = new List<Cliente>();
            List<Pedido> pedidos = new List<Pedido>();
            try
            {
                DataTable dt = await csvService.TransformaCSVParaDataTable(planilha);
                foreach(DataRow linha in dt.Rows)
                {
                    var novaLinha = csvService.TrataCamposLinha(linha);
                    novaLinha.AcceptChanges();
                    csvValidation.ValidaLinhaDataTable(novaLinha, dt.Rows.IndexOf(linha));

                    var cliente = (_clienteService.CriaObjetoCliente(novaLinha));
                    clienteValidation.ValidaObjetoCliente(cliente);
                    if(!(clientes.Any(a => a.CPFCNPJ == cliente.CPFCNPJ)))
                    {
                        clientes.Add(cliente);

                    }

                    var pedido = _service.CriaObjetoPedido(novaLinha);
                    pedido.Produto = await _produtoService.ObtemProdutoPorTitulo(novaLinha["Produto"].ToString());
                    pedido.ProdutoId = pedido.Produto.ProdutoId;
                    pedidoValidation.ValidaObjetoPedido(pedido);
                    pedidos.Add(pedido);

                }

                _clienteService.AdicionaVariosClientes(clientes);
                _service.AdicionaVariosPedidos(pedidos);

                _service.SalvaAlteracoes();

                return Ok();
                
            }
            catch (OnionSaServiceException onionExcp)
            {
                return BadRequest(onionExcp);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao tentar processar a planinha. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
    }
}