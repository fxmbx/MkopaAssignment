namespace MessageQueueLibrary.Contracts;
public interface ICustomHandler<TKey, TValue>
{
    Task HandleAsync(TKey key, TValue value);
}
