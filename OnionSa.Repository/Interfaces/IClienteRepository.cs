using OnionSa.Domain.Models;


namespace OnionSa.Repository.Interfaces
{
    public interface IClienteRepository
    {
        void InserirCliente<T>(T cliente) where T : Cliente;
        void InserirVariosClientes<T>(List<T> clientes) where T : Cliente;
        void AlterarCliente<T>(T cliente) where T : Cliente;
        void RemoverCliente<T>(T cliente) where T : Cliente;
        Task<Cliente> ObterClientePorDoc(long  documento);
        Task<List<Cliente>> ObterTodosOsClientes();
    }
}
