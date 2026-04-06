using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalBankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalBankApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<ContaBancaria> ContaBancarias {get; set;}
        public DbSet<Transacao> Transacaos {get; set;}
        
    }
}