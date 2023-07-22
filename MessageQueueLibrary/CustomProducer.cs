using MessageQueueLibrary.Contracts;
namespace MessageQueueLibrary;
public class CustomProducer<TKey, TValue> : ICustomProducer<TKey, TValue> where TValue : class
{
    public Task ProduceAsync(string topic, TKey key, TValue value)
    {
        throw new NotImplementedException();
    }
}
