using DeveloperChallenge.Domain.Entities;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        public void Add(BankTransaction bankTransaction)
        {
            throw new NotImplementedException();
        }

        public void BulkAdd(List<BankTransaction> bankTransactions)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<BankTransaction> GetAll()
        {
            throw new NotImplementedException();
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
