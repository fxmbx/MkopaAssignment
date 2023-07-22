using SmsService.Api.Controller;
using SmsService.Api.Middleware;
using Microsoft.AspNetCore.Http;

namespace SmsService.Test.System;
public class RequestLoggerMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ShouldLogRequestAndCallNextMiddleware()
    {
        // Arrange
        var loggerMock = new Mock<ILoggerService>();

        var middleware = new RequestLoggerMiddleware((innerHttpContext) => Task.FromResult(0), loggerMock.Object);
        var httpContextMock = new Mock<HttpContext>();
        string? nullabelString = null;

        // Set up a mock request with a sample path and query string
        var requestMock = new Mock<HttpRequest>();
        requestMock.Setup(r => r.Method).Returns("GET");
        requestMock.Setup(r => r.Path).Returns(new PathString("/api/test"));
        requestMock.Setup(r => r.QueryString).Returns(new QueryString("?param1=value1&param2=value2"));

        // Set up a mock response (not used in this test)
        var responseMock = new Mock<HttpResponse>();

        httpContextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
        httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);

        // Act
        await middleware.InvokeAsync(httpContextMock.Object);
        // Assert
        // Verify that LogInfo was called twice with the correct log messages
        loggerMock.Verify(
            logger => logger.LogInfo("Request: GET /api/test?param1=value1&param2=value2", nullabelString),
            Times.Once);

        loggerMock.Verify(
            logger => logger.LogInfo("Request Body: ", nullabelString),
            Times.Once);

        // Verify that the middleware does not modify the request body's position
        httpContextMock.Verify(
            c => c.Request.Body.Position,
            Times.Never);
    }
}