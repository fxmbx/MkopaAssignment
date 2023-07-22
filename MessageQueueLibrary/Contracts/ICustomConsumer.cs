namespace MessageQueueLibrary.Contracts;
public interface ICustomConsumer<TKey, TValue> where TValue : class
{
    Task Consume(string topic, CancellationToken stoppingToken);
}
