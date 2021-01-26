using System;

namespace XLN.Exceptions
{
    public class InvalidMovementStringException : Exception
    {
        private static string GetMessage(string movementString) => $"Invalid Movement String: {movementString}\nMovements must only use the following keys: < > ^";
        public InvalidMovementStringException(string movementString) : base(GetMessage(movementString)) { }
    }
}
