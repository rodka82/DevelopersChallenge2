using DeveloperChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Repositories.Interfaces
{
    public interface IBankTransactionRepository
    {
        void BulkAdd(List<BankTransaction> bankTransactions);
        void Add(BankTransaction bankTransaction);
        void Delete(int id);
        IQueryable<BankTransaction> GetAll();
        List<BankTransaction> GetByDate(DateTime date);
        List<BankTransaction> GetByDescription(string description);
    }
}
