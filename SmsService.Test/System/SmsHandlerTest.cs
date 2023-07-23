using SmsService.Infrastructure.Events.Handler;

namespace SmsService.Test.System;
public class SmsHandlerTest
{
    [Fact]
    public async Task HandleAsync_ShouldCallProduceAsyncWithCorrectParameters()
    {
        // Arrange
        var key = "test_key";
        var smsPayload = new SmsPayload();

        // Create a mock ICustomProducer<string, SmsPayload>
        var mockProducer = new Mock<ICustomProducer<string, SmsPayload>>();
        var mockSmsService = new Mock<ISmsService>();

        var smsHadler = new SmsHandler(mockProducer.Object, mockSmsService.Object);

        // Act
        await smsHadler.HandleAsync(key, smsPayload);

        // Assert
        // Verify that ProduceAsync was called once with the correct parameters
        mockProducer.Verify(
                producer => producer.ProduceAsync(Literals.SMS_SENT_TOPIC, key, smsPayload),
                Times.Once);
    }

    [Fact]
    public async Task HandleAsync_CallsProduceAsyncMultipleTimes_ShouldCallProduceAsyncForEachInvocation()
    {
        // Arrange
        string key1 = "key1";
        string key2 = "key2";
        var smsPayload1 = new SmsPayload();

        var smsPayload2 = new SmsPayload();

        var mockProducer = new Mock<ICustomProducer<string, SmsPayload>>();
        var mockSmsService = new Mock<ISmsService>();

        var sHandler = new SmsHandler(mockProducer.Object, mockSmsService.Object);

        // Act
        await sHandler.HandleAsync(key1, smsPayload1);
        await sHandler.HandleAsync(key2, smsPayload2);

        // Assert
        // Verify that ProduceAsync was called twice, once for each invocation
        mockProducer.Verify(
            producer => producer.ProduceAsync(Literals.SMS_SENT_TOPIC, key1, smsPayload1),
            Times.Once);
        mockProducer.Verify(
            producer => producer.ProduceAsync(Literals.SMS_SENT_TOPIC, key2, smsPayload2),
            Times.Once);
    }

}
