using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionSa.Domain.Interfaces;
using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Exceptions;
using OnionSa.Repository.Interfaces;


namespace OnionSa.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository<Cliente>
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Cliente> _dbSet;
        public ClienteRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Cliente>();
        }
        public async void AlterarCliente(Cliente cliente)
        {
            try
            {
                _dbSet.Update(cliente);
                await _cntxt.SaveChangesAsync();
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

        public async void InserirCliente(Cliente cliente)
        {
            try
            {
                await _dbSet.AddAsync(cliente);
                await _cntxt.SaveChangesAsync();
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

        public async Task<Cliente> ObterClientePorDoc(int documento)
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

        public void RemoverCliente(Cliente cliente)
        {
            try
            {
                _dbSet.Remove(cliente);
                _cntxt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
    }
}
