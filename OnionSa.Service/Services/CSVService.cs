using Microsoft.AspNetCore.Http;
using OnionSa.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Service.Services
{
    public class CSVService
    {

        /// <summary>
        /// Método responsável por obter o IFormFile e converter em um DataTable.
        /// </summary>
        /// <param name="planilha"></param>
        /// <returns>Retorna os dados da planilha em DataTable</returns>
        /// <exception cref="OnionSaServiceException"></exception>
        public async Task<DataTable> TransformaCSVParaDataTable(IFormFile planilha)
        {
            DataTable dt = new DataTable();

            try
            {
                //Objeto reader do planilha enviada.
                using (StreamReader stream = new StreamReader(planilha.OpenReadStream()))
                {
                    //Pega a primeira linha para obter os cabeçalhos da planilha
                    string[] cabecalhos = stream.ReadLine().Split(',');
                    foreach (string cabecalho in cabecalhos)
                    {
                        dt.Columns.Add(cabecalho);
                    }

                    //O laço é executado até o final da leitura
                    while (!stream.EndOfStream)
                    {
                        //Cada linha é lida
                        string[] linha = stream.ReadLine().Split(',');
                        DataRow novaLinha = dt.NewRow();
                        for (int i = 0; i < cabecalhos.Length; i++)
                        {
                            //E em seguida cada coluna
                            novaLinha[i] = linha[i];
                        }
                        dt.Rows.Add(novaLinha);
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar ler a planilha. Valide os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por tratar os campos de um DataRow.
        /// </summary>
        /// <param name="linha"></param>
        /// <returns>Retorna o DataRow tratado.</returns>
        public DataRow TrataCamposLinha(DataRow linha)
        {
            DataRow novaLinha = linha;
            try
            {
                linha["Documento"] = TrataCampoDocumento(linha.ItemArray[0].ToString());
                linha["CEP"] = TrataCEP(linha.ItemArray[2].ToString());
                linha.AcceptChanges();

                return novaLinha;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Trata o campo documento.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Retorna o dado sem os caracteres especiais.</returns>
        private string TrataCampoDocumento(string doc) 
        { 
            string novoDoc = doc.Replace("-", "").Replace(".", "").Replace("/", "").Trim();
            return novoDoc;
        }

        /// <summary>
        /// Trata o campo CEP.
        /// </summary>
        /// <param name="cep"></param>
        /// <returns>Retorna o dado sem os caracteres especiais.</returns>
        private string TrataCEP(string cep) 
        {
            string novoCEP = cep.Replace("-", "");
            return novoCEP;
        }

    }
}
