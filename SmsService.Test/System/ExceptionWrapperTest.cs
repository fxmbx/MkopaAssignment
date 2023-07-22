using SmsService.Infrastructure.CustomException;
using System.Net;

namespace SmsService.Test.System;
public class ExceptionWrapperTest
{
    [Fact]
    public async Task CallRepositoryAsync_SuccessfulExecution_ShouldReturnServiceResponse()
    {
        // Arrange
        var expectedResult = new ServiceResponse<int> { Success = true, Data = 42 };
        var mockLogger = new Mock<ILoggerService>();
        var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

        // Create a mock repository method that returns the expected result
        Func<Task<ServiceResponse<int>>> repositoryMethod = () => Task.FromResult(expectedResult);

        // Act
        var result = await exceptionWrapper.CallRepositoryAsync(repositoryMethod);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task CallRepositoryAsync_CustomException_ShouldHandleAndLogException()
    {
        // Arrange
        var expectedException = new ResuableCustomException("Custom Exception Message", HttpStatusCode.Conflict);
        var mockLogger = new Mock<ILoggerService>();
        var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

        // Create a mock repository method that throws the custom exception
        Func<Task<ServiceResponse<int>>> repositoryMethod = () => throw expectedException;

        // Act
        var result = await exceptionWrapper.CallRepositoryAsync(repositoryMethod);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(expectedException.GetStatusCode(), result.StatusCode);
        Assert.Equal(expectedException.Message, result.Description);

        // Verify that the logger was called with the correct parameters
        mockLogger.Verify(
            logger => logger.LogError(expectedException.Message, It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CallRepositoryAsync_GenericException_ShouldHandleAndLogException()
    {
        // Arrange
        var expectedException = new Exception("Generic Exception Message");
        var mockLogger = new Mock<ILoggerService>();
        var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

        // Create a mock repository method that throws a generic exception
        Func<Task<ServiceResponse<int>>> repositoryMethod = () => throw expectedException;

        // Act
        var result = await exceptionWrapper.CallRepositoryAsync(repositoryMethod);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        Assert.Equal(expectedException.Message, result.Description);

        // Verify that the logger was called with the correct parameters
        mockLogger.Verify(
            logger => logger.LogError(expectedException.Message, It.IsAny<string>()),
            Times.Once);
    }

}
