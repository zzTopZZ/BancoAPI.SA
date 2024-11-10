using BancoAPI.SA.Data;
using BancoAPI.SA.Dto.Cliente;
using BancoAPI.SA.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.SA.Services.Cliente
{
    public class ClienteService : IClienteInterface
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<ClienteModel>> BuscarClientePorCPF(string cpf)
        {
            ResponseModel<ClienteModel> resposta = new ResponseModel<ClienteModel>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.CPF == cpf);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    return resposta;
                }

                resposta.Dados = cliente;
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ClienteModel>> BuscarClientePorId(int Id)
        {
            ResponseModel<ClienteModel> resposta = new ResponseModel<ClienteModel>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == Id);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    return resposta;
                }

                resposta.Dados = cliente;
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ClienteModel>>> ListarClientes()
        {
            ResponseModel<List<ClienteModel>> resposta = new ResponseModel<List<ClienteModel>>();
            try
            {
                var clientes = await _context.Clientes.ToListAsync();

                resposta.Dados = clientes;
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ClienteModel>>> CriarCliente(ClienteModel clienteModel)
        {
            ResponseModel<List<ClienteModel>> resposta = new ResponseModel<List<ClienteModel>>();

            try
            {
                clienteModel.CPF = clienteModel.CPF.Replace(".","").Replace("-","");

                _context.Add(clienteModel);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Clientes.ToListAsync();
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<List<ClienteModel>>> EditarCliente(ClienteModel clienteModel)
        {
            ResponseModel<List<ClienteModel>> resposta = new ResponseModel<List<ClienteModel>>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == clienteModel.Id);

                if (cliente == null)
                {
                    resposta.Mensagem = "Nenhum cliente encontrado!";
                    return resposta;
                }

                cliente.Nome = clienteModel.Nome;
                cliente.CPF = clienteModel.CPF;
                cliente.DataNascimento = clienteModel.DataNascimento;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Clientes.ToListAsync();
                resposta.Mensagem = "Cliente alterado com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ClienteModel>>> ExcluirCliente(int IdCliente)
        {
            ResponseModel<List<ClienteModel>> resposta = new ResponseModel<List<ClienteModel>>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == IdCliente);

                if (cliente == null)
                {
                    resposta.Mensagem = "Nenhum cliente encontrado!";
                    return resposta;
                }

                _context.Remove(cliente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Clientes.ToListAsync();
                resposta.Mensagem = "Cliente removido com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
