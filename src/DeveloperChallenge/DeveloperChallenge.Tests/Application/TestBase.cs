using DeveloperChallenge.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Tests.Application
{
    public class TestBase
    {
        protected string GetDummyDataFile()
        {
            return $"{TestFolderFinder.GetTestDataFolder("Application\\DummyData")}\\extrato1.ofx";
        }
    }
}
