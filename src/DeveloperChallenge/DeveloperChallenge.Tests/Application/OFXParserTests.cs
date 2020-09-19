﻿using DeveloperChallenge.Application;
using DeveloperChallenge.Domain.Entities;
using DeveloperChallenge.Tests.Utils;
using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace DeveloperChallenge.Tests.Application
{
    public class OFXParserTests
    {
        [Fact]
        public void ShouldConvertToList()
        {
            var parser = new OFXParser();
            var list = parser.ConvertToList(GetDummyDataFile());
            Assert.True(list is List<BankTransaction>);
        }

        [Fact]
        public void ShouldReadOnlyTheNodeContent()
        {
            var parser = new OFXParser();
            var transaction = parser.ConvertToList(GetDummyDataFile()).First();
            Assert.True(transaction.Description.IndexOf("<MEMO>") < 0);
        }

        [Fact]
        public void ShouldReadDateTimeCorrectly()
        {
            var parser = new OFXParser();
            var wrongDateTime = new DateTime(1, 1, 1, 0, 0, 0);
            var dateTime = parser.ConvertToList(GetDummyDataFile()).First().Date;
            Assert.False(dateTime == wrongDateTime);
        }

        [Fact]
        public void ShouldReadAmoutCorrectly()
        {
            var parser = new OFXParser();
            var amount = parser.ConvertToList(GetDummyDataFile()).First().Amount;
            Assert.True(amount != 0);
        }

        private string GetDummyDataFile()
        {
            return $"{TestFolderFinder.GetTestDataFolder("Application\\DummyData")}\\extrato1.ofx";
        }
    }
}