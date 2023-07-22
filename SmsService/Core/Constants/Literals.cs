namespace SmsService.Core.Constants;
public class Literals
{
    public const string REQUIRED_FIELD = "Field is Required";
    public const string FAILED = "failed";
    public const string SUCCESS = "success";
    public const string PHONE_NUMBER_REG_EX = @"\+\d{1,3}\s?\(\d{3}\)\s?\d{3}-\d{4}";
    public const string INVALID_PHONE_FORMAT = "invalid phone number. Follow this format. +<CountryCode> can be 1 to 3 digits, <AreaCode> is 3 digits, <FirstThreeDigits> is 3 digits, and <LastFourDigits> is 4 digits";

    public const string SMS_TOPIC = "sms_topic";
    public const string SMS_SENT_TOPIC = "sms_sent";
    public const string SMS_DLQ_TOPIC = "sms_dlq";
    public const string BEGIN_CONSUMPTION = "comsuming from {0}";


    public const int MAX_RETRY_ATTEMPTS = 3;

}
