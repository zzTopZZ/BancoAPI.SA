using Azure;
using BancoAPI.SA.Models;

namespace BancoAPI.SA.Services.Cliente
{
    public interface IClienteInterface
    {
        Task<ResponseModel<List<ClienteModel>>> ListarClientes();
        Task<ResponseModel<ClienteModel>> BuscarClientePorId(int Id);
        Task<ResponseModel<ClienteModel>> BuscarClientePorCPF(string cpf);
        Task<ResponseModel<List<ClienteModel>>> CriarCliente(ClienteModel clienteModel);
        Task<ResponseModel<List<ClienteModel>>> EditarCliente(ClienteModel clienteModel);
        Task<ResponseModel<List<ClienteModel>>> ExcluirCliente(int IdCliente);
    }
}
