

using MessageQueueLibrary.Contracts;

namespace SmsService.Infrastructure.Events.Handler;
public class SmsHandler : ICustomHandler<string, SmsPayload>
{
    private readonly ICustomProducer<string, SmsPayload> _producer;
    private readonly ISmsService _smsService;

    public SmsHandler(ICustomProducer<string, SmsPayload> producer, ISmsService _smsService)
    {
        _producer = producer;
        this._smsService = _smsService;
    }
    public Task HandleAsync(string key, SmsPayload value)
    {
        _smsService.SendSmsAsync(value);
        _producer.ProduceAsync(Literals.SMS_SENT_TOPIC, key, value);
        return Task.CompletedTask;
    }
}
