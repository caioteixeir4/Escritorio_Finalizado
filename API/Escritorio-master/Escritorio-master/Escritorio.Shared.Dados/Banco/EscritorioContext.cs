using Escritorio.Shared.Modelos.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Dados.Banco
{
    public class EscritorioContext : DbContext
    {
        //permite alterações no banco de dados CRUD
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Propriedade> Propriedades { get; set; }
        public DbSet<Recibo> Recibos { get; set; }

        //referencia para o banco
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EscritorioV20;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //abre e fecha conexao do banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("Escritorio.API"))
                .UseLazyLoadingProxies();
        }
    }
}
