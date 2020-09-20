using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperChallenge.Presentation.Models
{
    public class FileUploadModel
    {
        public IFormFile FormFile { get; set; }
    }
}
