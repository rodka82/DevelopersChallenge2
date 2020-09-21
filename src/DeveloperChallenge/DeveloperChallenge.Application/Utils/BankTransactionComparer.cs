using DeveloperChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Utils
{
    public class BankTransactionComparer : IEqualityComparer<BankTransaction>
    {
        public bool Equals(BankTransaction x, BankTransaction y)
        {
            var sameType = x.Type == y.Type;
            var sameDate = x.Date == y.Date;
            var sameAmount = x.Amount == y.Amount;
            var sameDescription = x.Description == y.Description;

            return (sameType && sameDate && sameAmount && sameDescription);
        }

        public int GetHashCode(BankTransaction obj)
        {
            return obj == null || obj.Description == null ? 0 : $"{obj.Description}{obj.Date}{obj.Amount}{obj.Type}".GetHashCode();

        }
    }
}
