using MessageQueueLibrary.Messages;
namespace MessageQueueLibrary.Contracts;
/// <summary>
/// The DeadLetterQueue is used handle messages that failed to be processed
/// within the concrete implementation for the ICustomConsumer or ICustomProducer, it is advisable to SendToDeadLetterQueue in the catch block after a defined number of retries
/// </summary>
public interface IDeadLetterQueue
{
    void SendToDeadLetterQueue<T>(DLQMessage<T> dLQMessage);
    void ProcessDeadLetterMessages();
}
