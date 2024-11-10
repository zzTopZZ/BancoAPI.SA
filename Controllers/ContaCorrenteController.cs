using BancoAPI.SA.Dto.ContaCorrente;
using BancoAPI.SA.Models;
using BancoAPI.SA.Services.ContaCorrente;
using BancoAPI.SA.Services.Transacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.SA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteInterface _contacorrenteInterface;

        public ContaCorrenteController(IContaCorrenteInterface contacorrenteInterface)
        {
            _contacorrenteInterface = contacorrenteInterface;
        }

        [HttpGet("ListarContaCorrente")]
        public async Task<ActionResult<ResponseModel<List<ContaCorrenteModel>>>> ListarTransacao()
        {
            var contascorrentes = await _contacorrenteInterface.ListarContaCorrente();
            return Ok(contascorrentes);
        }

        [HttpPost("CriarContaCorrente")]
        public async Task<ActionResult<ResponseModel<ContaCorrenteModel>>> CriarContaCorrente(ContaCorrenteCriacaoDto contacorrenteDTO)
        {
            var contacorrente = await _contacorrenteInterface.CriarContaCorrente(contacorrenteDTO);
            return Ok(contacorrente);
        }

        [HttpGet("BuscarContaCorrentePorId/{Id}")]
        public async Task<ActionResult<ResponseModel<ContaCorrenteModel>>> BuscarContaCorrentePorId(int Id)
        {
            var contacorrente = await _contacorrenteInterface.BuscarContaCorrentePorId(Id);
            return Ok(contacorrente);
        }
    }
}
