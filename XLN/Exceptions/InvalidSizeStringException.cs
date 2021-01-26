using System;

namespace XLN.Exceptions
{
    public class InvalidSizeStringException : Exception
    {
        private static string GetMessage(string sizeString) => $"Invalid Size String: {sizeString}\nSize must be in the format: X Y";
        public InvalidSizeStringException(string sizeString) : base(GetMessage(sizeString)) { }
    }
}
