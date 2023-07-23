namespace MessageQueueLibrary.Contracts;
public interface ICustomHandler<TKey, TMessage>
{
    Task HandleAsync(TKey key, TMessage msg);
}
