using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalBankApi.Data;
using GlobalBankApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ContasController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ContasController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var candidatos = _context.ContaBancarias.ToList();
            return Ok(candidatos);      
        }
        [HttpPost]
        public async Task<ActionResult<ContaBancaria>> PostConta(ContaBancaria conta)
        {
            if(conta.Saldo < 0)
            {
                return BadRequest("Saldo inicial não pode ser negativo!");
                
            }
             
            _context.ContaBancarias.Add(conta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = conta.Id}, conta);
        }

        
    }
}