using MessageQueueLibrary.Contracts;

namespace MessageQueueLibrary;
public class CustomConsumer<TKey, TValue> : ICustomConsumer<TKey, TValue> where TValue : class
{
    public Task Consume(string topic, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
