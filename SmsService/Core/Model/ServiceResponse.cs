using System.Net;

namespace SmsService.Core.Model;
public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public string? Description { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}
