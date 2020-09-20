using DeveloperChallenge.Application.Parser;
using DeveloperChallenge.Application.Services;
using DeveloperChallenge.Domain.Entities;
using DeveloperChallenge.Domain.Enums;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace DeveloperChallenge.Tests.Application
{
    public class BankTransactionServiceTests : TestBase
    {
        [Fact]
        public void ShouldSaveWithoutDuplication()
        {
            var repository = ConfigureBankTransactionRepositoryMock();
            var parser = new OFXParser();
            var fileService = new FileService();
            var service = new BankTransactionsService(parser,fileService,repository);
            var newBankTransaction = GenerateMockedBankTransactions().FirstOrDefault();
            newBankTransaction.Id = 0;

            var savedBankTransactions = service.SaveBankTransactions(new List<BankTransaction> { newBankTransaction });

            Assert.True(savedBankTransactions.Count() == 0);
        }

        private IBankTransactionRepository ConfigureBankTransactionRepositoryMock()
        {
            var bankTransactionRepositoryMock = new Mock<IBankTransactionRepository>();
            List<BankTransaction> mockedBankTransactions = GenerateMockedBankTransactions();
            bankTransactionRepositoryMock.Setup(t => t.GetAll()).Returns(mockedBankTransactions);
            return bankTransactionRepositoryMock.Object;
        }

        private List<BankTransaction> GenerateMockedBankTransactions()
        {
            return new List<BankTransaction>
            {
              new BankTransaction
               {
                   Id = 1,
                   Type = BankTransactionType.Credit,
                   Date = new DateTime(2020,1,1,10,0,0,0),
                   Amount = 1000.00,
                   Description = "Salário"
               },
               new BankTransaction
               {
                   Id = 2,
                   Type = BankTransactionType.Debit,
                   Date = new DateTime(2020,1,1,12,0,0,0),
                   Amount = 100.00,
                   Description = "Conta de luz"
               },
               new BankTransaction
               {
                   Id = 3,
                   Type = BankTransactionType.Debit,
                   Date = new DateTime(2020,1,1,14,0,0,0),
                   Amount = 10.00,
                   Description = "Sorvete"
               }
            };
        }

        private List<IFormFile> GenerateMockedIFormFiles()
        {
            var fileMock = new Mock<IFormFile>();
            var content = File.OpenText(GetDummyDataFile()).ReadToEnd(); ;
            var fileName = "extrato1.ofx";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return new List<IFormFile> { fileMock.Object } ;
        } 
    }
}
