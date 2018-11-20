namespace SatlockApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EncodeBase64
    {

        public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        public string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);

            string returnValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
    }
}
