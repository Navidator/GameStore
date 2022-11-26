using System.Runtime.Serialization;

namespace GameStore.CustomExceptions
{
    public class DoesNotExistException : CustomException
    {
        public DoesNotExistException() : base() { }

        public DoesNotExistException(string message) : base(message) { }

        public DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
