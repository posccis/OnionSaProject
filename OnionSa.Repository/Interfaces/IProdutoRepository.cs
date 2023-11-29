using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IProdutoRepository<T> where T : Produto
    {
        void InserirProduto(T produto);
        void AlterarProduto(T produto);
        void RemoverProduto(T produto);
        Task<T> ObterProdutoPorId(int  id);
        Task<List<T>> ObterTodosOsProdutos();
    }
}
