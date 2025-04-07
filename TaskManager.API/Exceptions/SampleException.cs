using System.Net;

namespace TaskManager.API.Exceptions
{
    public class SampleException : CustomException
    {
        public SampleException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}