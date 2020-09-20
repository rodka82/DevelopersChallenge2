using DeveloperChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Services.Interfaces
{
    public interface IBankTransactionService
    {
        void SaveBankTransactions(List<IFormFile> files, string directory);
        List<BankTransaction> Get();
    }
}
