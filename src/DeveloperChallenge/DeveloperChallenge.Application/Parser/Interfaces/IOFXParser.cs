using DeveloperChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Parser.Interfaces
{
    public interface IOFXParser
    {
        List<BankTransaction> Parse(string ofxFilePath);
    }
}