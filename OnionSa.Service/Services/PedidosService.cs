using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using OnionSa.Domain.Interfaces;
using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Interfaces;
using OnionSa.Repository.Repositories;
using OnionSa.Service.Exceptions;
using OnionSa.Service.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _repo;
        private readonly PedidoValidation PedidoValidation;
        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
            PedidoValidation = new PedidoValidation();
        }

        /// <summary>
        /// Método que constrói um objeto pedido através de um DataRow.
        /// </summary>
        /// <param name="linha"></param>
        /// <returns cref="Pedido">Retorna o objeto Pedido extraido DataRow inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public Pedido CriaObjetoPedido(DataRow linha)
        {
            try
            {
                Pedido pedido = new Pedido()
                {
                    NumeroDoPedido = int.Parse(linha[4].ToString()),
                    CPFCNPJ = linha[0].ToString(),
                    Cep = linha[2].ToString(),
                    Data = DateTime.Parse(linha[5].ToString()),
                };

                
                return pedido;
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
        /// Adiciona vários pedidos.
        /// </summary>
        /// <param name="pedidos"></param>
        /// <exception cref="OnionSaServiceException"></exception>
        public void AdicionaVariosPedidos(List<Pedido> pedidos)
        {
            try
            {
                PedidoValidation.ValidaListaPedidos(pedidos);
                _repo.InserirVariosPedidos(pedidos);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir a lista de pedidos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
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

        /// <summary>
        /// Método responsável por realizar a requisição para a API da 'Viacep' e receber os dados relacionados ao CEP do pedido.
        /// </summary>
        /// <param name="cep"></param>
        /// <returns>Os dados relacionados ao cep do pedido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<DadosCep> RetornaDadosDoCep(string cep) 
        {
            string urlCep = $"https://viacep.com.br/ws/{cep}/json/";
            DadosCep dadosCep = null;
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(urlCep);
                    if(response.IsSuccessStatusCode)
                    {
                        var conteudo = await response.Content.ReadAsStringAsync();
                        dadosCep = JsonConvert.DeserializeObject<DadosCep>(conteudo);
                    }

                    return dadosCep;
                }
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao obter os detalhes do cep. Revise os dados enviados ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
            }
        }

        /// <summary>
        /// Método responsável por identificar a região de origem do CEP inserido.
        /// </summary>
        /// <param name="uf"></param>
        /// <returns>A região do CEP inserido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<string> IdentificaRegiaoPedido(string uf) 
        {
            #region Listas com as regiões
            // Estados da região Norte
            List<string> estadosNorte = new List<string> { "AC", "AP", "AM", "PA", "RO", "RR", "TO" };

            // Estados da região Nordeste
            List<string> estadosNordeste = new List<string> { "AL", "BA", "CE", "MA", "PB", "PE", "PI", "RN", "SE" };

            // Estados da região Centro-Oeste
            List<string> estadosCentroOeste = new List<string> { "DF", "GO", "MT", "MS" };

            // Estados da região Sudeste
            List<string> estadosSudeste = new List<string> { "ES", "MG", "RJ", "SP" };

            // Estados da região Sul
            List<string> estadosSul = new List<string> { "PR", "RS", "SC" };
            #endregion Listas com as regiões

            try
            {


                //Verificação se o UF do CEP está dentro da lista de alguma região.
                if (estadosSudeste.Any(e => e == uf))
                {
                    //Retorna a região.
                    return "Sudeste";
                }
                else if (estadosSul.Any(e => e == uf))
                {
                    return "Sul";
                }else if (estadosCentroOeste.Any(e => e == uf))
                {
                    return "CentroOeste";
                }
                else if (estadosNorte.Any(e => e == uf))
                {
                    return "Norte";

                }
                else if (estadosNordeste.Any(e => e == uf))
                {
                    return "Nordeste";
                }
                //Caso o UF não seja localizado dentro de nenhuma das listas, uma exceção é lançada.
                else
                {
                    throw new OnionSaServiceException("Não foi possivel localizar o UF do CEP disponibilizado. Valide se o CEP foi inserido corretamente ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.");
                }
            }
            catch (OnionSaServiceException onionExc)
            {
                throw onionExc;
            }

            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar identificar a região do CEP. Revise os dados inseridos ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
            }

        }

        /// <summary>
        /// Método responsável por calcular o preço final do produto baseando-se na taxa de frete cobrada para sua região.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>Retorna o valor do produto somado ao seu frete.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<double> CalculaPrecoFinal(Pedido pedido, string regiao) 
        {

            double precoFinal = 0;

            //Porcentagem utilizada para calcular o frete para cada região
            double taxaFreteSudeste = 0.1;
            double taxaFreteNorteNordeste = 0.3;
            double taxaFreteSulCentro = 0.2;

            try
            {


                //Verificação se o regiao do CEP está dentro da lista de alguma região.
                if(regiao == "Sudeste" )
                {
                    //Calculo do preço sendo somado á texa de frete.
                    precoFinal = pedido.Produto.Preco + (taxaFreteSudeste * pedido.Produto.Preco);
                }
                else if(regiao == "Sul" ||  regiao == "CentroOeste")
                {
                    precoFinal = pedido.Produto.Preco + (taxaFreteSulCentro * pedido.Produto.Preco);
                }
                else if(regiao == "Norte" || regiao == "Nordeste")
                {
                    precoFinal = pedido.Produto.Preco + (taxaFreteNorteNordeste * pedido.Produto.Preco);
                }
                return precoFinal;

            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao calcular o valor final do pedido. Revise os dados inseridos ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
            }

        }

        /// <summary>
        /// Método responsável por definir a data de entrega do pedido baseado na sua região.
        /// </summary>
        /// <param name="dataDoPedido"></param>
        /// <param name="regiao"></param>
        /// <returns>A data de entrega do pedido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<DateTime> CalculoDataDeEntrega(DateTime dataDoPedido, string regiao)
        {
            //Prazo de entrega baseado na região.
            int prazoNorteNordeste = 10;
            int prazoCentrSul = 5;
            int prazoSudeste = 1;

            DateTime dataEntrega = dataDoPedido;
            try
            {
                if (regiao == "Sudeste")
                {
                    //Executa o método responsável pelo calculo da data de entrega.
                    dataEntrega = DefineData(dataDoPedido, prazoSudeste);
                }
                else if (regiao == "Sul" || regiao == "CentroOeste")
                {
                    dataEntrega = DefineData(dataDoPedido, prazoCentrSul);
                }
                else if (regiao == "Norte" || regiao == "Nordeste")
                {
                    dataEntrega = DefineData(dataDoPedido, prazoNorteNordeste);
                }
                return dataEntrega;
            }
            catch(OnionSaServiceException onionExc)
            {
                throw onionExc;
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar definir a data de entrega do pedido; Valide os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}");
            }
        }

        /// <summary>
        /// Método que adiciona o prazo de entrega á data em que o pedido foi realizado.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dias"></param>
        /// <returns>Retorna a data de entrega do pedido.</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        private DateTime DefineData(DateTime data, int dias) 
        {
            int i = 1;
            DateTime dataEntrega = data;
            try
            {
                //O laço irá ser executado enquanto a quantidade de dias úteis do prazo não tiver sido somada.
                while (i < dias)
                {
                    dataEntrega = dataEntrega.AddDays(1);
                    //Caso o dia adicionado seja útil, é somado á váriavel i mais 1, caso não, continua o mesmo valor até o proximo dia útil.
                    if (!VerificaDiaUtil(dataEntrega)) i ++;
                }

                return dataEntrega;
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar definir a data de entrega do pedido. Valide os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}");
            }
        }
        /// <summary>
        /// Verifica se um dia na semana é um dia útil ou um final de semana.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Retorna true caso o dia seja um final de semana e false caso seja um dia útil.</returns>
        private bool VerificaDiaUtil(DateTime data)
        {
            return data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday;
        }

        /// <summary>
        /// Método responsável por salvar as alterações realizadas.
        /// </summary>
        /// <exception cref="OnionSaServiceException"></exception>
        public void SalvaAlteracoes()
        {
            try
            {
                _repo.Save();

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao salvar as alterações. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }




    }

}
