using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IClienteRepository<T> where T : Cliente
    {
        void InserirCliente(T cliente);
        void AlterarCliente(T cliente);
        void RemoverCliente(T cliente);
        Task<T> ObterClientePorDoc(int  documento);
        Task<List<T>> ObterTodosOsClientes();
    }
}
