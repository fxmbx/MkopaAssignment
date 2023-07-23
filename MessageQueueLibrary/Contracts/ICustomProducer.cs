namespace MessageQueueLibrary.Contracts;
public interface ICustomProducer<in TKey, in TMessage> where TMessage : class
{
    Task ProduceAsync(string topic, TKey key, TMessage msg);
}