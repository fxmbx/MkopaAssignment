
using System.Net;

namespace SmsService.Infrastructure.CustomException;

public class ExceptionWrapper<T>
{

    private readonly ILoggerService _logger;
    public ExceptionWrapper(ILoggerService logger)
    {
        _logger = logger;
    }

    public async Task<ServiceResponse<T>> CallRepositoryAsync(Func<Task<ServiceResponse<T>>> repositoryMethod)
    {
        ServiceResponse<T> response = new()
        {
            Success = false,
            Message = Literals.FAILED,
            StatusCode = HttpStatusCode.InternalServerError,

        };

        try
        {
            response = await repositoryMethod();
        }
        catch (NotImplementedException ex)
        {
            response.Description = ex.Message;
        }
        catch (ResuableCustomException ex)
        {
            response.StatusCode = ex.GetStatusCode();
            response.Description = ex.Message;
            _logger.LogError(ex.Message, ex.Source ?? ex.StackTrace);
        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Description = ex.Message;
            _logger.LogError(ex.Message, ex.Source ?? ex.StackTrace);

        }
        return response;
    }

}