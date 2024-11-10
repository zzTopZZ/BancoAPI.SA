using BancoAPI.SA.Data;
using BancoAPI.SA.Dto.Extrato;
using BancoAPI.SA.Dto.Transacao;
using BancoAPI.SA.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BancoAPI.SA.Services.Transacao
{
    public class TransacaoService : ITransacaoInterface
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<TransacaoModel>>> BuscarTransacaoPorCPF(string cpf)
        {
            ResponseModel<List<TransacaoModel>> resposta = new ResponseModel<List<TransacaoModel>>();

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.CPF == cpf);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    return resposta;
                }

                var transacao = await _context.Transacoes.Where(x=> x.contaCorrente.ClienteId == cliente.Id).ToListAsync();

                if (transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrado!";
                    return resposta;
                }

                resposta.Dados = transacao;
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

        public async Task<ResponseModel<TransacaoModel>> BuscarTransacaoPorId(int Id)
        {
            ResponseModel<TransacaoModel> resposta = new ResponseModel<TransacaoModel>();

            try
            {
                var transacao = await _context.Transacoes.FirstOrDefaultAsync(x => x.Id == Id);

                if (transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrado!";
                    return resposta;
                }

                resposta.Dados = transacao;
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

        public async Task<ResponseModel<TransacaoModel>> CriarTransacao(TransacaoCriacaoDto transacaoDto)
        {
            ResponseModel<TransacaoModel> resposta = new ResponseModel<TransacaoModel>();

            try
            {
                var contaCorrente = await _context.ContaCorrentes.FirstOrDefaultAsync(x => x.Id == transacaoDto.ContaCorrenteId);

                if (contaCorrente == null)
                {
                    resposta.Mensagem = "Conta corrente não encontrado!";
                    return resposta;
                }

                var tipoTransacao = await _context.TipoTransacao.FirstOrDefaultAsync(x => x.Id == transacaoDto.TipoTransacaoId);

                if (transacaoDto.TipoTransacaoId == 1)
                {
                    contaCorrente.Saldo = contaCorrente.Saldo + transacaoDto.Valor;
                }
                else
                {
                    contaCorrente.Saldo = contaCorrente.Saldo - transacaoDto.Valor;
                }

                var transacao = new TransacaoModel()
                {
                    ContaCorrenteId = transacaoDto.ContaCorrenteId,
                    TipoTransacaoId = transacaoDto.TipoTransacaoId,
                    DataTransacao = DateTime.Now,
                    Descricao = transacaoDto.Descricao,
                    Valor = transacaoDto.Valor,
                    contaCorrente = contaCorrente,
                    tipoTransacao = tipoTransacao,
                };

                _context.Add(transacao);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Transacoes.FirstOrDefaultAsync();
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

        public Task<ResponseModel<List<TransacaoModel>>> EditarTransacao(TransacaoModel transacaoModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<TransacaoModel>>> ExcluirTranscao(int IdTransacao)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<TransacaoModel>>> ListarTransacao()
        {
            ResponseModel<List<TransacaoModel>> resposta = new ResponseModel<List<TransacaoModel>>();

            try
            {
                var transacoes = await _context.Transacoes
                    .Include(a => a.contaCorrente)
                    .Include(a => a.tipoTransacao)
                    .Include(a => a.contaCorrente.Cliente)
                    .ToListAsync();

                resposta.Dados = transacoes;
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

        public async Task<ResponseModel<List<ExtratoPdfDto>>> GerarExtratoPDF([FromQuery] int contaCorrenteId, [FromQuery] int filtroDias)
        {
            ResponseModel<List<ExtratoPdfDto>> resposta = new ResponseModel<List<ExtratoPdfDto>>();

            try
            {

                var dias = new[] { "5", "10", "15", "20" };

                bool naoExiste = dias.Contains(filtroDias.ToString());

                if (!dias.Contains(filtroDias.ToString()))
                {
                    resposta.Mensagem = "Periodo invalido!";
                    return resposta;
                }

                var dataFiltro = DateTime.Now.AddDays(-filtroDias);
                var transacoes = await _context.Transacoes
                    .Include(a => a.contaCorrente)
                    .Include(a => a.tipoTransacao)
                    .Include(a => a.contaCorrente.Cliente)
                    .Where(t => t.ContaCorrenteId == contaCorrenteId) // && t.DataTransacao >= dataFiltro)
                    .OrderBy(t => t.DataTransacao)
                    .ToListAsync();

                var extratoLista = new List<ExtratoPdfDto>();

                foreach (var transacao in transacoes)
                {
                    var extrato = new ExtratoPdfDto()
                    {
                        Valor = transacao.Valor,
                        DataTransacaoStr = transacao.DataTransacao.ToString("dd/MM"),
                        TipoTransacaoDescricao = transacao.tipoTransacao.Descricao,
                    };
                    extratoLista.Add(extrato);
                };

                var teste = GeneratePdfStatement(transacoes);

                resposta.Dados = extratoLista;
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

        public byte[] GeneratePdfStatement(List<TransacaoModel> transacaoModel)
        {

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var document = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(ms)))                    
                    {
                        var pdfDocument = new iText.Layout.Document(document);
                        pdfDocument.Add(new iText.Layout.Element.Paragraph("Extrato Bancário"));
                        pdfDocument.Add(new iText.Layout.Element.Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy")}"));

                        foreach (var transacao in transacaoModel)
                        {
                            pdfDocument.Add(new iText.Layout.Element.Paragraph($"Data: {transacao.DataTransacao.ToString("dd/MM/yyyy")}, " +
                                                                              $"Tipo: {transacao.tipoTransacao.Descricao}, " +
                                                                              $"Valor: {transacao.Valor:C}"));
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
