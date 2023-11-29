using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IPedidoRepository<T> where T : Pedido
    {
        void InserirPedido(T pedido);
        void AlterarPedido(T pedido);
        void RemoverPedido(T pedido);
        Task<T> ObterPedidoPorNumero(int  numero);
        Task<List<T>> ObterTodosOsPedidos();
    }
}
