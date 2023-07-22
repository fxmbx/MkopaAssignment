namespace MessageQueueLibrary.Messages;
public class DLQMessage<T>
{
    public string? Topic { get; set; }
    public T? Message { get; set; }
    public int RetryAttempts { get; set; }
}
