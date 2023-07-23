using SmsService.Infrastructure.Services;

namespace SmsService.Test.System;
public class SmsServiceTests
{

    [Fact]
    public async Task SendSmsAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var loggerMock = new Mock<ILoggerService>();

        var smsService = new SmsServiceImp(loggerMock.Object);
        var smsPayload = new SmsPayload
        {
            RecipientPhoneNumber = "+2349167641670",
            TextMessage = "Hello, this is a test SMS."
        };
        await Assert.ThrowsAsync<NotImplementedException>(() => smsService.SendSmsAsync(smsPayload));
    }

}
