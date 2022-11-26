using System;
using System.Runtime.Serialization;

namespace GameStore.CustomExceptions
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
