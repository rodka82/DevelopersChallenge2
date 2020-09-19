using DeveloperChallenge.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperChallenge.Application.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ParseOfxDate(string ofxDateTime)
        {
            var transactionDateTime = new DateTime();
            try
            {
                if (ofxDateTime.Length < 14)
                    return transactionDateTime;

                var ss = Int32.Parse(ofxDateTime.Substring(12, 2));
                var mm = Int32.Parse(ofxDateTime.Substring(10, 2));
                var hh = Int32.Parse(ofxDateTime.Substring(8, 2));
                var dd = Int32.Parse(ofxDateTime.Substring(6, 2));
                var MM = Int32.Parse(ofxDateTime.Substring(4, 2));
                var yyyy = Int32.Parse(ofxDateTime.Substring(0, 4));

                transactionDateTime = new DateTime(yyyy, MM, dd, hh, mm, ss);
                return transactionDateTime;
            }
            catch
            {
                throw new OfxParseException("Unable to parse date");
            }
        }
    }
}
