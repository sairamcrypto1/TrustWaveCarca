using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
//using TrustWaveCarca.Services 
public interface ISmsService
{
    Task SendOtpAsync(string phoneNumber, string otpCode);
}

public class SmsService : ISmsService
{
    private readonly IConfiguration _configuration;

    public SmsService(IConfiguration configuration)
    {
        _configuration = configuration;

        // Initialize Twilio client using values from the configuration
        TwilioClient.Init(
            _configuration["Twilio:AccountSID"],
            _configuration["Twilio:AuthToken"]
        );
    }

    public async Task SendOtpAsync(string phoneNumber, string otpCode)
    {
        var message = $"Your verification code is: {otpCode}";

        try
        {
            // Send the SMS message
            var messageResponse = await MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(phoneNumber),
                from: new Twilio.Types.PhoneNumber(_configuration["Twilio:FromPhoneNumber"]),
                body: message
            );

            // Optional: Check if the message was sent successfully
            if (messageResponse.ErrorCode != null)
            {
                // Log or handle errors as needed
                throw new Exception($"Error sending SMS: {messageResponse.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions, such as logging the error
            throw new ApplicationException("SMS sending failed.", ex);
        }
    }
}
