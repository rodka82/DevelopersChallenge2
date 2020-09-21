using DeveloperChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Domain.Entities
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public BankTransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}