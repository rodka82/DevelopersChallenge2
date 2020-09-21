using DeveloperChallenge.Domain.Entities;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Repositories.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<BankTransaction> _dbSet;

        public BankTransactionRepository(DbContext niboContext)
        {
            _dbContext = niboContext;
            _dbSet = _dbContext.Set<BankTransaction>();
        }

        public void Add(BankTransaction bankTransaction)
        {
            _dbContext.Add(bankTransaction);
            _dbContext.SaveChanges();
        }

        public void BulkAdd(List<BankTransaction> bankTransactions)
        {
            foreach (var transaction in bankTransactions)
                Add(transaction);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BankTransaction> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public List<BankTransaction> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<BankTransaction> GetByDescription(string description)
        {
            throw new NotImplementedException();
        }
    }
}
