using DeveloperChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Services.Interfaces
{
    public interface IBankTransactionService
    {
        List<BankTransaction> SaveBankTransactions(List<BankTransaction> bankTransactions);
        List<BankTransaction> Parse(List<string> fileLocations);
        List<BankTransaction> Get();
    }
}
