using System.Runtime.Serialization;

namespace GameStore.CustomExceptions
{
    public class CouldNotRegisterUserException : CustomException
    {
        public CouldNotRegisterUserException() : base() { }

        public CouldNotRegisterUserException(string message) : base(message) { }

        protected CouldNotRegisterUserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
