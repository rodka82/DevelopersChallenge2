using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperChallenge.Api.DTO;
using DeveloperChallenge.Application.Services.Interfaces;
using DeveloperChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionsController : ControllerBase
    {
        
        private readonly IBankTransactionService _transactionService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public BankTransactionsController(IBankTransactionService transactionService, IFileService fileService, IMapper mapper)
        {
            _transactionService = transactionService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_transactionService.Get().Select(t => _mapper.Map<BankTransaction, BankTransactionDTO>(t)));
        }

        [HttpPost]
        public IActionResult SaveBankTransactions([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                var fileLocations = _fileService.Upload(files, "ReceivedFiles");
                var transactions = _transactionService.Parse(fileLocations);
                var savedTransactions = _transactionService.SaveBankTransactions(transactions);

                if (savedTransactions.Count > 0)
                    return new OkObjectResult(savedTransactions.Select(t => _mapper.Map<BankTransaction, BankTransactionDTO>(t)));
                else
                    return Ok($"Warning: No transactions were saved");
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

    }
}
