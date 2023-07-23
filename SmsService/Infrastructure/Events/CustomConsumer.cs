using MessageQueueLibrary.Contracts;
using MessageQueueLibrary.Messages;

namespace SmsService.Infrastructure.Events;
public class CustomConsumer<TKey, TMessage> : ICustomConsumer<TKey, TMessage> where TMessage : class
{
    private readonly IDeadLetterQueue _deadLetterQueue;
    private readonly ILoggerService _loggerService;
    private readonly ICustomHandler<TKey, TMessage> _customHandler;


    public CustomConsumer(IDeadLetterQueue _deadLetterQueue, ILoggerService _loggerService, ICustomHandler<TKey, TMessage> _customHandler)
    {
        this._deadLetterQueue = _deadLetterQueue;
        this._loggerService = _loggerService;
        this._customHandler = _customHandler;
    }
    public Task Consume(string topic, CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                //TODO: Implement comsuming logic and store response in result variable 
                // var result = consuming logic response
                // if (result != null)
                // {
                //      ProcessMessageWithRetryAsync(T, V);
                // }
                break;
            }
            catch (Exception ex)
            {
                _loggerService.LogWarning("Error Consume :: ", ex);
                break;

            }
        }

        return Task.CompletedTask;

    }

    private async Task ProcessMessageWithRetryAsync(TKey key, TMessage msg, CancellationToken stoppingToken)
    {
        int retryCounter = 1;
        while (stoppingToken.IsCancellationRequested && retryCounter < Literals.MAX_RETRY_ATTEMPTS)
        {
            try
            {
                await _customHandler.HandleAsync(key, msg);
                return;
            }
            catch (Exception ex)
            {
                _loggerService.LogWarning($"Error Processing Message, Attempting {retryCounter} Retry :: ", ex);

                await Task.Delay(TimeSpan.FromSeconds(5 * retryCounter), stoppingToken);
                retryCounter++;
            }
        }
        if (retryCounter >= Literals.MAX_RETRY_ATTEMPTS)
        {
            _loggerService.LogWarning("ProcessMessageWithRetryAsync :: ", "Max retry attempts reached. Sending message to Dead-Letter Queue.");
            MoveToDeadLetterQueue(msg);
        }
    }

    private void MoveToDeadLetterQueue(TMessage msg)
    {
        //TODO:Add code to move the message to the Dead Letter Queue,
        var dLQMessage = new DLQMessage<TMessage>()
        {
            Topic = Literals.SMS_DLQ_TOPIC,
            Message = msg,
            RetryAttempts = Literals.MAX_RETRY_ATTEMPTS
        };
        _deadLetterQueue.SendToDeadLetterQueue(dLQMessage);

    }
}
