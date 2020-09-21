using DeveloperChallenge.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeveloperChallenge.Application.Services
{
    public class FileService : IFileService
    {
        public List<string> Upload(List<IFormFile> files, string targetFolder)
        {
            Directory.CreateDirectory(targetFolder);

            var filePaths = new List<string>();
            files.ForEach(file =>
            {
                if (file.Length <= 0) return;
                var fileName = $"{DateTime.Now.Ticks}{file.FileName}";
                var filePath = Path.Combine(targetFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    filePaths.Add(filePath);
                }
            });

            return filePaths;
        }
    }
}
