using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        void InserirProduto<T>(T produto) where T : Produto;
        void AlterarProduto<T>(T produto) where T : Produto;
        void RemoverProduto<T>(T produto) where T : Produto;
        Task<Produto> ObterProdutoPorId(int  id);
        Task<Produto> ObterProdutoPorTitulo(string  titulo);
        Task<List<Produto>> ObterTodosOsProdutos();
    }
}
