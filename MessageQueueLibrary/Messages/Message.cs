
namespace MessageQueueLibrary.Messages;
public class Message<TMessage>
{
    public Dictionary<string, TMessage>? MessageBody { get; set; }
}
