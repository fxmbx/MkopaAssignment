using SmsService.Infrastructure.Events;
namespace SmsService.Test.System;
public class SmsConsumerTests
{
    [Fact]
    public async Task ExecuteAsync_SuccessfulConsumption_ShouldLogInfo()
    {
        // Arrange
        var mockICustomConsumer = new Mock<ICustomConsumer<string, SmsPayload>>();
        var mockLoggerService = new Mock<ILoggerService>();
        var cancellationToken = new CancellationToken();

        mockICustomConsumer.Setup(c => c.Consume(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var sConsumer = new SmsConsumer(mockICustomConsumer.Object, mockLoggerService.Object);

        // Act
        await sConsumer.StartConsuming(cancellationToken);

        // Assert
        mockLoggerService.Verify(
            logger => logger.LogInfo("ExecuteAsync :: ", string.Format(Literals.BEGIN_CONSUMPTION, Literals.SMS_TOPIC)),
            Times.Once);

        mockICustomConsumer.Verify(
            c => c.Consume(Literals.SMS_TOPIC, cancellationToken),
            Times.Once);

        mockLoggerService.Verify(
            logger => logger.LogError(It.IsAny<string>(), It.IsAny<Exception>()),
            Times.Never); // No errors should be logged
    }

    [Fact]
    public async Task ExecuteAsync_ExceptionDuringConsumption_ShouldLogError()
    {
        // Arrange
        var mockICustomConsumer = new Mock<ICustomConsumer<string, SmsPayload>>();
        var mockLoggerService = new Mock<ILoggerService>();
        var cancellationToken = new CancellationToken();

        // Mock the ICustomConsumer to throw an exception during consumption
        mockICustomConsumer.Setup(c => c.Consume(It.IsAny<string>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Simulated consumption error"));

        var sConsumer = new SmsConsumer(mockICustomConsumer.Object, mockLoggerService.Object);

        // Act
        await sConsumer.StartConsuming(cancellationToken);

        // Assert
        mockLoggerService.Verify(
            logger => logger.LogInfo("ExecuteAsync :: ", string.Format(Literals.BEGIN_CONSUMPTION, Literals.SMS_TOPIC)),
            Times.Once);

        mockICustomConsumer.Verify(
            c => c.Consume(Literals.SMS_TOPIC, cancellationToken),
            Times.Once);

        mockLoggerService.Verify(
            logger => logger.LogError($"Error during consumption :: ", It.IsAny<Exception>()),
            Times.Once); // An error should be logged
    }
}
