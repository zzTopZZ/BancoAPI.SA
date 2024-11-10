using BancoAPI.SA.Dto.Extrato;
using BancoAPI.SA.Dto.Transacao;
using BancoAPI.SA.Models;
using BancoAPI.SA.Services.Cliente;
using BancoAPI.SA.Services.Transacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.SA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {

        private readonly ITransacaoInterface _transacaoInterface;

        public TransacaoController(ITransacaoInterface transacaoInterface)
        {
            _transacaoInterface = transacaoInterface;
        }

        [HttpGet("ListarTransacao")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> ListarTransacao()
        {
            var transacoes = await _transacaoInterface.ListarTransacao();
            return Ok(transacoes);
        }

        [HttpPost("CriarTransacao")]
        public async Task<ActionResult<ResponseModel<TransacaoModel>>> CriarTransacao(TransacaoCriacaoDto transacaoDto)
        {
            var transacao = await _transacaoInterface.CriarTransacao(transacaoDto);
            return Ok(transacao);
        }

        [HttpGet("ListarTransacaoPorCPF/{cpf}")]
        public async Task<ActionResult<ResponseModel<List<TransacaoModel>>>> ListarTransacaoPorCPF(string cpf)
        {
            var transacao = await _transacaoInterface.BuscarTransacaoPorCPF(cpf);
            return Ok(transacao);
        }

        [HttpGet("GerarExtratoPDF/{ContaCorrenteId}/{FIltroDias}")]
        public async Task<ActionResult<ResponseModel<List<ExtratoPdfDto>>>> GerarExtratoPDF(int ContaCorrenteId, int FIltroDias)
        {
            var transacao = await _transacaoInterface.GerarExtratoPDF(ContaCorrenteId, FIltroDias);
            return Ok(transacao);
        }

    }
}
