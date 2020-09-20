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
        private readonly IBankTransactionRepository _repository;

        public BankTransactionsService(IOFXParser parser, IFileService fileService, IBankTransactionRepository repository)
        {
            _parser = parser;
            _fileService = fileService;
            _repository = repository;
        }

        public List<BankTransaction> Get()
        {
            return _repository.GetAll();
        }

        public void SaveBankTransactions(List<IFormFile> files, string directory)
        {
            var uploadedFilesLocations = _fileService.Upload(files, directory);
            var transactionsToSave = MergeWithExistingTransactions(uploadedFilesLocations);
            _repository.BulkAdd(transactionsToSave);
        }

        private List<BankTransaction> MergeWithExistingTransactions(List<string> uploadedFilesLocations)
        {
            var incomingTransactions = new List<BankTransaction>();

            foreach (var fileLocation in uploadedFilesLocations)
                incomingTransactions.Union(_parser.Parse(fileLocation));

            var dataBaseTransactions = _repository.GetAll();

            var transactionsToSave = dataBaseTransactions
                .Union(incomingTransactions, new BankTransactionComparer())
                .Where(t => t.Id == 0)
                .ToList();

            return transactionsToSave;
        }
    }
}