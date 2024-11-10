using BancoAPI.SA.Data;
using BancoAPI.SA.Dto.ContaCorrente;
using BancoAPI.SA.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.SA.Services.ContaCorrente
{
    public class ContaCorrenteService : IContaCorrenteInterface
    {
        private readonly AppDbContext _context;

        public ContaCorrenteService(AppDbContext context)
        {
            _context = context;
        }

        public Task<ResponseModel<ContaCorrenteModel>> BuscarContaCorrentePorCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<ContaCorrenteModel>> BuscarContaCorrentePorId(int Id)
        {
            ResponseModel<ContaCorrenteModel> resposta = new ResponseModel<ContaCorrenteModel>();

            try
            {
                var contaCorrente = await _context.ContaCorrentes
                    .Include(a => a.Cliente)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (contaCorrente == null)
                {
                    resposta.Mensagem = "Conta corrente não encontrado!";
                    return resposta;
                }

                resposta.Dados = contaCorrente;
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

        public async Task<ResponseModel<ContaCorrenteModel>> CriarContaCorrente(ContaCorrenteCriacaoDto contacorrenteDTO)
        {
            ResponseModel<ContaCorrenteModel> resposta = new ResponseModel<ContaCorrenteModel>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == contacorrenteDTO.IdCliente);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    return resposta;
                }

                var conta = new ContaCorrenteModel()
                {
                    ClienteId = contacorrenteDTO.IdCliente,
                    Agencia = contacorrenteDTO.Agencia,
                    Saldo = contacorrenteDTO.Saldo,
                    Cliente = cliente,
                };

                _context.Add(conta);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.ContaCorrentes.FirstOrDefaultAsync();
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

        public Task<ResponseModel<List<ContaCorrenteModel>>> EditarContaCorrente(ContaCorrenteModel contacorrenteModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<ContaCorrenteModel>>> ExcluirContaCorrente(int IdContaCorrente)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<ContaCorrenteModel>>> ListarContaCorrente()
        {
            ResponseModel<List<ContaCorrenteModel>> resposta = new ResponseModel<List<ContaCorrenteModel>>();
            try
            {
                var ContasCorretes = await _context.ContaCorrentes
                    .Include(a => a.Cliente)
                    .ToListAsync();

                resposta.Dados = ContasCorretes;
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
