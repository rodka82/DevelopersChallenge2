using DeveloperChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories.DbConfiguration.EFCore
{
    public class NiboContext : DbContext
    {
        public NiboContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(DatabaseConnection.ConnectionConfiguration
                                                    .GetConnectionString("DefaultConnection"));
            }
        }

        public NiboContext(DbContextOptions<NiboContext> options) : base(options)
        {
        }

        public DbSet<BankTransaction> BankTransactions { get; set; }
    }
}
