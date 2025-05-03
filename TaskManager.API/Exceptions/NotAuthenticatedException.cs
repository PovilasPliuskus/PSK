using System.Net;

namespace TaskManager.API.Exceptions
{
    public class NotAuthenticatedException : CustomException
    {
        public NotAuthenticatedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
}