
namespace SmsService.Core.Contracts;
public interface ISmsService
{
    Task<ServiceResponse<string>> SendSmsAsync(SmsPayload payload);
}
