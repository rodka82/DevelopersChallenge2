using System;
using System.Collections.Generic;
using System.Linq;
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

        public BankTransactionsController(IBankTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            try
            {
                _transactionService.(files, "ReceivedFiles");
                return Ok(new { files.Count });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpPost("save")]
        public IActionResult SaveBankTransactions([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                _transactionService.SaveBankTransactions(files, "ReceivedFiles");
                return Ok(new { files.Count });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

    }
}
