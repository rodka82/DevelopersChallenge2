using DeveloperChallenge.Application.Exceptions;
using DeveloperChallenge.Application.Helpers;
using DeveloperChallenge.Domain.Entities;
using DeveloperChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeveloperChallenge.Application
{
    public class OFXParser
    {
        public List<BankTransaction> ConvertToList(string ofxFilePath)
        {
            var transactionsValues = GetTransactionsValues(ofxFilePath);
            var bankTransactions = new List<BankTransaction>();

            foreach (var transaction in transactionsValues)
            {
                if(TryGenerateBankTransaction(transaction, out BankTransaction bankTransaction))
                    bankTransactions.Add(bankTransaction);
            }

            return bankTransactions;
        }

        private bool TryGenerateBankTransaction(string transaction, out BankTransaction bankTransaction)
        {
            if (transaction.Replace(System.Environment.NewLine, "") != "")
            {
                bankTransaction = new BankTransaction
                {
                    Type = GetType(transaction),
                    Date = GetDateTime(transaction),
                    Amount = GetAmount(transaction),
                    Description = GetNodeValue(transaction, "<MEMO>")
                };
                return true;
            }
            bankTransaction = null;
            return false;
        }

        private BankTransactionType GetType(string transaction)
        {
            var type = GetNodeValue(transaction, "<TRNTYPE>");
            var canParse = Enum.TryParse(type, true, out BankTransactionType transactionType);

            if (!canParse)
                throw new OfxParseException("Unable to parse Type");

            return transactionType;
        }

        private DateTime GetDateTime(string transaction)
        {
            var ofxDateTime = GetNodeValue(transaction, "<DTPOSTED>");
            return DateTimeHelper.ParseOfxDate(ofxDateTime);
        }

        private double GetAmount(string transaction)
        {
            var amount = GetNodeValue(transaction, "<TRNAMT>");
            var canParse = double.TryParse(amount, out double transactionAmount);
            if (!canParse)
                throw new OfxParseException("Unable to parse Amount");
            return transactionAmount;
        }

        public string GetNodeValue(string transaction, string nodeName)
        {
            var nodeValue = Regex.Match(transaction, $"^({nodeName}[^\n\r]+)", RegexOptions.Multiline).Value.Replace(nodeName,"");
            return nodeValue;
        }

        private static List<string> GetTransactionsValues(string ofxFilePath)
        {
            CheckFileExists(ofxFilePath);
            var body = GetBodyFromOfxFile(ofxFilePath);
            return GetTransactionsFromOfxBody(body);
        }

        private static List<string> GetTransactionsFromOfxBody(string body)
        {
            string[] nodesSeparators = { "<STMTTRN>", "</STMTTRN>" };
            var transactionsValues = body.Split(nodesSeparators, StringSplitOptions.None)
                .Skip(1)
                .ToList();
            transactionsValues = transactionsValues.Take(transactionsValues.Count() - 1).Where(x => !string.IsNullOrEmpty(x)).ToList();
            return transactionsValues;
        }

        private static void CheckFileExists(string ofxFilePath)
        {
            if (!File.Exists(ofxFilePath))
                throw new FileNotFoundException("OFX file not found: " + ofxFilePath);
        }

        private static string GetBodyFromOfxFile(string ofxFilePath)
        {
            var ofxContent = File.OpenText(ofxFilePath).ReadToEnd();
            var start = ofxContent.IndexOf("<OFX>");
            var end = ofxContent.IndexOf("</OFX>");
            var body = ofxContent.Substring(start, end - start);
            return body;
        }
    }
}