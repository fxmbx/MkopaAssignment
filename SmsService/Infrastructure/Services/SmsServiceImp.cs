
namespace SmsService.Infrastructure.Services;
public class SmsServiceImp : ISmsService
{
    Task<ServiceResponse<string>> ISmsService.SendSmsAsync(SmsPayload payload)
    {
        throw new NotImplementedException();
    }
}
