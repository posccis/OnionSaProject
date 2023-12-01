﻿using Microsoft.AspNetCore.Http;
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
        public async Task<DataTable> TransformaCSVParaDataTable(IFormFile planilha)
        {
            DataTable dt = new DataTable();

            try
            {
                using (StreamReader stream = new StreamReader(planilha.OpenReadStream()))
                {
                    string[] cabecalhos = stream.ReadLine().Split(',');
                    foreach (string cabecalho in cabecalhos)
                    {
                        dt.Columns.Add(cabecalho);
                    }

                    while (!stream.EndOfStream)
                    {
                        string[] linha = stream.ReadLine().Split(',');
                        DataRow novaLinha = dt.NewRow();
                        for (int i = 0; i < cabecalhos.Length; i++)
                        {
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
    }
}
