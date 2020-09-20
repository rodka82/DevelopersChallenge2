using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DeveloperChallenge.Application.Services.Interfaces;
using DeveloperChallenge.Tests.Utils;
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

        public BankTransactionsController(IBankTransactionService transactionService, IFileService fileService)
        {
            _transactionService = transactionService;
            _fileService = fileService;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("save")]
        public IActionResult SaveBankTransactions([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                var fileLocations = _fileService.Upload(files, "ReceivedFiles");
                var transactions = _transactionService.Parse(fileLocations);
                var savedTransactions = _transactionService.SaveBankTransactions(transactions);

                if (savedTransactions.Count > 0)
                    return new OkObjectResult(savedTransactions);
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
