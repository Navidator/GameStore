using System.Runtime.Serialization;

namespace GameStore.CustomExceptions
{
    public class AlreadyExistEsception : CustomException
    {
        public AlreadyExistEsception() : base() { }

        public AlreadyExistEsception(string message) : base(message) { }

        protected AlreadyExistEsception(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
