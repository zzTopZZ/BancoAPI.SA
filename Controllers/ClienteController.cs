using BancoAPI.SA.Models;
using BancoAPI.SA.Services.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.SA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteInterface _clienteInterface;

        public ClienteController(IClienteInterface clienteInterface)
        {
            _clienteInterface = clienteInterface;
        }

        [HttpGet("ListarClientes")]
        public async Task<ActionResult<ResponseModel<List<ClienteModel>>>> ListarClientes()
        {
            var clientes = await _clienteInterface.ListarClientes();
            return Ok(clientes);    
        }

        [HttpGet("ListarClientePorId/{Id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> ListarClientePorId(int Id)
        {
            var cliente = await _clienteInterface.BuscarClientePorId(Id);
            return Ok(cliente);
        }

        [HttpGet("ListarClientePorCPF/{cpf}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> ListarClientePorCPF(string cpf)
        {
            var cliente = await _clienteInterface.BuscarClientePorCPF(cpf);
            return Ok(cliente);
        }

        [HttpPost("CriarCliente")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> CriarCliente(ClienteModel clienteModel)
        {
            var cliente = await _clienteInterface.CriarCliente(clienteModel);
            return Ok(cliente);
        }

        [HttpDelete("ExcluirCliente/{Id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> ExcluirCliente(int Id)
        {
            var cliente = await _clienteInterface.ExcluirCliente(Id);
            return Ok(cliente);
        }

        [HttpPut("EditarCliente")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> EditarCliente(ClienteModel clienteModel)
        {
            var cliente = await _clienteInterface.EditarCliente(clienteModel);
            return Ok(cliente);
        }

    }
}
