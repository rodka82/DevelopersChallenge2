using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Services.Interfaces
{
    public interface IFileService
    {
        List<string> Upload(List<IFormFile> files, string subDirectory);
    }
}
