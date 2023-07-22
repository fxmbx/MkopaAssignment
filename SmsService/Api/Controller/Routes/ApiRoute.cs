namespace SmsService.Api.Controller;
public class ApiRoute
{


    private const string root = "api";
    private const string version = "/v1";
    private const string service = "/mkopa-sms";
    private const string Base = $"{root}{service}{version}";
    public static class PostUrl
    {
        public const string SEND_SMS = $"{Base}/send";
    }


}
