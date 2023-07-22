namespace MessageQueueLibrary.Contracts;
public interface ICustomProducer<in TKey, in Tvalue> where Tvalue : class
{
    Task ProduceAsync(string topic, TKey key, Tvalue value);

}