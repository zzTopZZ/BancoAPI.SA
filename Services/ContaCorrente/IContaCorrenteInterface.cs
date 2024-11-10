using BancoAPI.SA.Dto.ContaCorrente;
using BancoAPI.SA.Models;

namespace BancoAPI.SA.Services.ContaCorrente
{
    public interface IContaCorrenteInterface
    {
        Task<ResponseModel<List<ContaCorrenteModel>>> ListarContaCorrente();
        Task<ResponseModel<ContaCorrenteModel>> BuscarContaCorrentePorId(int Id);
        Task<ResponseModel<ContaCorrenteModel>> BuscarContaCorrentePorCPF(string cpf);
        Task<ResponseModel<ContaCorrenteModel>> CriarContaCorrente(ContaCorrenteCriacaoDto contacorrenteDTO);
        Task<ResponseModel<List<ContaCorrenteModel>>> EditarContaCorrente(ContaCorrenteModel contacorrenteModel);
        Task<ResponseModel<List<ContaCorrenteModel>>> ExcluirContaCorrente(int IdContaCorrente);
    }
}
