
namespace SmsService.Infrastructure.Services;
public class SmsServiceImp : ISmsService
{
    private readonly ILoggerService _loggerService;
    public SmsServiceImp(ILoggerService _loggerService)
    {
        this._loggerService = _loggerService;
    }
    public async Task<ServiceResponse<string>> SendSmsAsync(SmsPayload payload)
    {
        ServiceResponse<string> response = new()
        {
            Message = Literals.SUCCESS,
            Status = Enum.GetName(StatusEnum.DONE),
            StatusCode = System.Net.HttpStatusCode.OK,
        };

        await SendSmsToThirdPartyAsync(payload);


        _loggerService.LogInfo("SendSmsAsync ::", $"sms send to {payload.RecipientPhoneNumber}");

        response.Data = payload.RecipientPhoneNumber;

        return response;

    }

    private Task SendSmsToThirdPartyAsync(SmsPayload smsPayload)
    {
        throw new NotImplementedException();
    }
}
