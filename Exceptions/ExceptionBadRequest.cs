using System.Runtime.Serialization;

namespace AccountApi.Exceptions
{
    [Serializable]
    internal class ExceptionBadRequest : Exception
    {

        public ExceptionBadRequest(string? message) : base(message)
        {
        }
    }
}