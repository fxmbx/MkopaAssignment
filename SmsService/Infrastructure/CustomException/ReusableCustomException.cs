
using System.Net;
using System.Runtime.Serialization;

namespace SmsService.Infrastructure.CustomException;
public class ResuableCustomException : Exception
{
    private HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
    public ResuableCustomException() { }

    public ResuableCustomException(string message, HttpStatusCode code)
        : base(message)
    {
        SetStatusCode(code);
    }

    protected ResuableCustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public void SetStatusCode(HttpStatusCode code)
    {
        statusCode = code;
    }
    public HttpStatusCode GetStatusCode()
    {
        return statusCode;
    }
}
