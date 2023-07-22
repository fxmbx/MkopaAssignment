using Microsoft.AspNetCore.Mvc;
using SmsService.Infrastructure.CustomException;

namespace SmsService.Api.Controller
{
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly ISmsService _smsService;

        public SmsController(ILoggerService _logger, ISmsService _smsService)
        {
            this._logger = _logger;
            this._smsService = _smsService;
        }


        [HttpPost(ApiRoute.PostUrl.SEND_SMS)]
        public async Task<IActionResult> Post([FromBody] SmsPayload smsPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ExceptionWrapper<string> exceptionWrapper = new(_logger);
            var response = await exceptionWrapper.CallRepositoryAsync(() =>
            {
                return _smsService.SendSmsAsync(smsPayload);
            });
            return StatusCode((int)response.StatusCode!, response);
        }


    }
}