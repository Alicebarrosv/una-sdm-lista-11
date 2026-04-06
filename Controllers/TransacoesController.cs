using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalBankApi.Data;
using GlobalBankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GlobalBankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController :ControllerBase
    {
        private readonly AppDbContext _context;
        
        public TransacoesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var transacaos = _context.Transacaos.ToList();
            return Ok(transacaos);
            
        }
        [HttpPost]
        public IActionResult Post(Transacao transacao)
        {
            var conta = _context.ContaBancarias.FirstOrDefault(c => c.Id == transacao.ContaId);
            if (conta == null)
            {
                return NotFound("Conta não encontrada");               
            }
            if (transacao.Tipo == "Saque")
            {
                conta.Saldo -= transacao.Valor;
            }
            else if (transacao.Tipo == "Deposito")
            {
                conta.Saldo += transacao.Valor;
            }
             
            _context.Transacaos.Add(transacao);
            _context.SaveChanges();
            string alerta = null;
            if(transacao.Valor > 10000)
            {
                alerta = $"Alerta: Transação de alto valor detectada para conta {conta.NumeroConta}";
            }
            return CreatedAtAction(nameof(Get), new { id = transacao.Id}, new {transacao, alerta});
            
        }

        
    }
}