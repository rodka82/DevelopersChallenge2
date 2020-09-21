using DeveloperChallenge.Application.Parser;
using DeveloperChallenge.Application.Parser.Interfaces;
using DeveloperChallenge.Application.Services.Interfaces;
using DeveloperChallenge.Application.Utils;
using DeveloperChallenge.Domain.Entities;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeveloperChallenge.Application.Services
{
    public class BankTransactionsService : IBankTransactionService
    {
        private readonly IOFXParser _parser;
        private readonly IFileService _fileService;
        public readonly IBankTransactionRepository _repository;

        public BankTransactionsService(IOFXParser parser, IFileService fileService, IBankTransactionRepository repository)
        {
            _parser = parser;
            _fileService = fileService;
            _repository = repository;
        }

        public List<BankTransaction> Get()
        {
            return _repository.GetAll().ToList();
        }

        public List<BankTransaction> SaveBankTransactions(List<BankTransaction> bankTransactions)
        {
            var transactionsToSave = MergeWithExistingTransactions(bankTransactions);
            if(transactionsToSave.Count > 0)
                _repository.BulkAdd(transactionsToSave);

            return transactionsToSave;
        }

        public List<BankTransaction> Parse(List<string> uploadedFilesLocations)
        {
            var incomingTransactions = new List<BankTransaction>();

            foreach (var fileLocation in uploadedFilesLocations)
                incomingTransactions = incomingTransactions.Concat(_parser.Parse(fileLocation)).ToList();

            return incomingTransactions;
        }

        private List<BankTransaction> MergeWithExistingTransactions(List<BankTransaction> incomingTransactions)
        {
            var dataBaseTransactions = _repository.GetAll().ToList();
            var transactionsToSave = MergeTransactions(incomingTransactions, dataBaseTransactions).Where(t => t.Id == 0).ToList();
            return transactionsToSave;
        }

        private static List<BankTransaction> MergeTransactions(List<BankTransaction> firstTransactionList, List<BankTransaction> secondTransactionList)
        {
            return secondTransactionList
                .Union(firstTransactionList, new BankTransactionComparer())
                .ToList();
        }
    }
}