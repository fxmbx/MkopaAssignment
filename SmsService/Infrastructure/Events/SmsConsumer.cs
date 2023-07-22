
using MessageQueueLibrary.Contracts;

namespace SmsService.Infrastructure.Events;
public class SmsConsumer : BackgroundService
{
    private readonly ICustomConsumer<string, SmsPayload> _consumer;
    private readonly ILoggerService _loggerService;



    public SmsConsumer(ICustomConsumer<string, SmsPayload> _consumer, ILoggerService _loggerService)
    {
        this._consumer = _consumer;
        this._loggerService = _loggerService;
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            _loggerService.LogInfo("ExecuteAsync :: ", string.Format(Literals.BEGIN_CONSUMPTION, Literals.SMS_TOPIC));
            await _consumer.Consume(Literals.SMS_TOPIC, cancellationToken);
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Error during consumption :: ", ex);

        }

    }
    public async Task StartConsuming(CancellationToken cancellationToken)
    {
        await ExecuteAsync(cancellationToken);
    }
}