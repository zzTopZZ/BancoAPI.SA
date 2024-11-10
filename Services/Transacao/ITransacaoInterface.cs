using BancoAPI.SA.Dto.Extrato;
using BancoAPI.SA.Dto.Transacao;
using BancoAPI.SA.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.SA.Services.Transacao
{
    public interface ITransacaoInterface
    {
        Task<ResponseModel<List<TransacaoModel>>> ListarTransacao();
        Task<ResponseModel<TransacaoModel>> BuscarTransacaoPorId(int Id);
        Task<ResponseModel<List<TransacaoModel>>> BuscarTransacaoPorCPF(string cpf);
        Task<ResponseModel<TransacaoModel>> CriarTransacao(TransacaoCriacaoDto transacaoDto);
        Task<ResponseModel<List<TransacaoModel>>> EditarTransacao(TransacaoModel transacaoModel);
        Task<ResponseModel<List<TransacaoModel>>> ExcluirTranscao(int IdTransacao);
        Task<ResponseModel<List<ExtratoPdfDto>>> GerarExtratoPDF([FromQuery] int contaCorrenteId, [FromQuery] int filtroDias);
    }
}
