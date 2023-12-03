﻿using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionSa.Domain.Interfaces;
using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Exceptions;
using OnionSa.Repository.Interfaces;


namespace OnionSa.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Cliente> _dbSet;
        public ClienteRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Cliente>();
        }
        /// <summary>
        /// M[etodo responsável por realizar o update de um cliente na tabela.
        /// </summary>
        /// <param name="cliente"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public void AlterarCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                _dbSet.Update(cliente);
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch(MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch(Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar atualizar o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }

        }

        /// <summary>
        /// Método responsável por realizar o insert de um cliente na tabela.
        /// </summary>
        /// <param name="cliente"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async void InserirCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                await _dbSet.AddAsync(cliente);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de cliente que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por realizar o insert de vários clientes na tabela.
        /// </summary>
        /// <param name="clientes"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async void InserirVariosClientes<T>(List<T> clientes) where T : Cliente
        {
            try
            {
                _dbSet.AddRange(clientes);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de cliente que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por realizar o select de um cliente através do seu CNPJ/CPF na tabela.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async Task<Cliente> ObterClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _dbSet.FirstOrDefaultAsync(x => x.CPFCNPJ == documento);
                return cliente;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por realizar o select retornar todos os clientes da tabela.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async Task<List<Cliente>> ObterTodosOsClientes()
        {
            try
            {
                var clientes = await _dbSet.ToListAsync();
                return clientes;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter todos os clientes. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por realizar o delete de um cliente na tabela.
        /// </summary>
        /// <param name="cliente"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public void RemoverCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                _dbSet.Remove(cliente);
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar remover o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
    }
}
