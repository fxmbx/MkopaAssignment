
namespace MessageQueueLibrary.Messages;
public class Message<TValue>
{
    public Dictionary<string, TValue>? MessageBody { get; set; }
}
