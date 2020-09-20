using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeveloperChallenge.Tests.Utils
{
    public static class DirectoryFinder
    {
        public static string GetTargetDirectory(string directoryName)
        {
            string startupPath = ApplicationEnvironment.ApplicationBasePath;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            var pos = pathItems.Reverse().ToList().FindIndex(x => string.Equals("bin", x));
            string projectPath = String.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - pos - 1));
            return Path.Combine(projectPath, directoryName);
        }
    }
}
