using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperChallenge.Presentation.Controllers
{
    public class UploadController : ControllerBase
    {
        private readonly IFileService _fileService;

        //[HttpPost("upload")]
        //public IActionResult UploadFile([FromForm(Name = "files")] List<IFormFile> files, string subDirectory)
        //{

        //}
    }
}