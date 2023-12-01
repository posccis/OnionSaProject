using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        void InserirPedido<T>(T pedido) where T : Pedido;
        void AlterarPedido<T>(T pedido) where T : Pedido;
        void RemoverPedido<T>(T pedido) where T : Pedido;
        Task<Pedido> ObterPedidoPorNumero(int  numero);
        Task<List<Pedido>> ObterTodosOsPedidos();
    }
}
