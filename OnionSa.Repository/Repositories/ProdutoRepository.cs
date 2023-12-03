using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionSa.Domain.Interfaces;
using OnionSa.Domain.Models;
using OnionSa.Repository.Context;
using OnionSa.Repository.Exceptions;
using OnionSa.Repository.Interfaces;


namespace OnionSa.Repository.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Produto> _dbSet;
        public ProdutoRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Produto>();
        }

        /// <summary>
        /// Método responsável por realizar o update de um produto dentro da tabela.
        /// </summary>
        /// <param name="produto"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public void AlterarProduto<T>(T produto) where T : Produto
        {
            try
            {
                _dbSet.Update(produto);
                _cntxt.SaveChanges();
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar atualizar o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }

        }


        /// <summary>
        /// Método responsável por realizar o insert de um produto dentro da tabela.
        /// </summary>
        /// <param name="produto"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async void InserirProduto<T>(T produto) where T : Produto
        {
            try
            {
                await _dbSet.AddAsync(produto);
                await _cntxt.SaveChangesAsync();
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de produto que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por retornar da tabela um produto especifico através do seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async Task<Produto> ObterProdutoPorId(int id)
        {
            try
            {
                var produto = await _dbSet.FirstOrDefaultAsync(x => x.ProdutoId == id);
                return produto;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Método responsável por retornar da tabela um produto especifico através do seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async Task<Produto> ObterProdutoPorTitulo(string titulo)
        {
            try
            {
                var produto = await _dbSet.FirstOrDefaultAsync(x => x.Titulo == titulo);
                return produto;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por retornar todos os produtos da tabela.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public async Task<List<Produto>> ObterTodosOsProdutos()
        {
            try
            {
                var produtos = await _dbSet.ToListAsync();
                return produtos;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter todos os produtos. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsável por realizar o delete de um porduto na tabela.
        /// </summary>
        /// <param name="produto"></param>
        /// <exception cref="OnionSaRepositoryException"></exception>
        public void RemoverProduto<T>(T produto) where T : Produto
        {
            try
            {
                _dbSet.Remove(produto);
                _cntxt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
    }
}
