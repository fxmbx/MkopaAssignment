namespace MessageQueueLibrary.Contracts;
//TODO: Implement ICustomConsumer. advicable to have the retry mechanism within the implementation 
public interface ICustomConsumer<TKey, TMessage> where TMessage : class
{
    Task Consume(string topic, Func demo, CancellationToken stoppingToken);
}
