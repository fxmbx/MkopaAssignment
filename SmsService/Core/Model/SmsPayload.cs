using System.ComponentModel.DataAnnotations;

namespace SmsService.Core.Model;
public class SmsPayload
{
    [Required(ErrorMessage = Literals.REQUIRED_FIELD)]
    [RegularExpression(Literals.PHONE_NUMBER_REG_EX, ErrorMessage = Literals.INVALID_PHONE_FORMAT)]

    public string RecipientPhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = Literals.REQUIRED_FIELD)]
    public string TextMessage { get; set; } = null!;
    public Dictionary<string, object>? Extras { get; set; }
}
