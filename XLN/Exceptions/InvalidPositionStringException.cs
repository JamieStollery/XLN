using System;

namespace XLN.Exceptions
{
    public class InvalidPositionStringException : Exception
    {
        private static string GetMessage(string positionString) => $"Invalid Position String: {positionString}\nPosition must be in the format: X Y D";
        public InvalidPositionStringException(string positionString) : base(GetMessage(positionString)) { }
    }
}
