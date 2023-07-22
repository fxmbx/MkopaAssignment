
namespace SmsService.Infrastructure.Services;
public class LoggerService : ILoggerService
{
    public void LogError<T>(string message, T? data = default)
    {
        throw new NotImplementedException();
    }

    public void LogInfo<T>(string messgae, T? data = default)
    {
        throw new NotImplementedException();
    }

    public void LogWarning<T>(string message, T? data = default)
    {
        throw new NotImplementedException();
    }
}
