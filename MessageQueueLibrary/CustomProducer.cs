using MessageQueueLibrary.Contracts;
namespace MessageQueueLibrary;
public class CustomProducer<TKey, TMessage> : ICustomProducer<TKey, TMessage> where TMessage : class
{
    public Task ProduceAsync(string topic, TKey key, TMessage msg)
    {
        throw new NotImplementedException();
    }
}
