

using MessageQueueLibrary.Contracts;

namespace SmsService.Infrastructure.Events.Handler;
public class SmsHandler : ICustomHandler<string, SmsPayload>
{
    private readonly ICustomProducer<string, SmsPayload> _producer;
    public SmsHandler(ICustomProducer<string, SmsPayload> producer)
    {
        _producer = producer;
    }
    public Task HandleAsync(string key, SmsPayload value)
    {
        _producer.ProduceAsync(Literals.SMS_SENT_TOPIC, key, value);
        return Task.CompletedTask;
    }
}
