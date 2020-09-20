using DeveloperChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Api.DTO
{
    public class BankTransactionDTO
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
