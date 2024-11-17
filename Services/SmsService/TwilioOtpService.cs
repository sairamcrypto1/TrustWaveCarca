using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TrustWaveCarca.Services.SmsService
{
        public class TwilioOtpService : IOtpService
        {
            private readonly string _accountSid;
            private readonly string _authToken;
            private readonly string _fromPhoneNumber;
            private readonly Dictionary<string, string> _otpStore; // Temporary store for OTPs, for demo purposes

            public TwilioOtpService(IConfiguration configuration)
            {
                _accountSid = configuration["Twilio:AccountSid"];
                _authToken = configuration["Twilio:AuthToken"];
                _fromPhoneNumber = configuration["Twilio:FromPhoneNumber"];
                _otpStore = new Dictionary<string, string>();

                TwilioClient.Init(_accountSid, _authToken);
            }

            public async Task<bool> SendOtpAsync(string phoneNumber, string otp)
            {
                try
                {
                    var message = await MessageResource.CreateAsync(
                        to: new PhoneNumber(phoneNumber),
                        from: new PhoneNumber(_fromPhoneNumber),
                        body: $"Your verification code is: {otp}");

                    // Store OTP for the phone number temporarily (replace with persistent storage in production)
                    _otpStore[phoneNumber] = otp;
                    return message.ErrorCode == null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending OTP: {ex.Message}");
                    return false;
                }
            }

            public Task<bool> VerifyOtpAsync(string phoneNumber, string otp)
            {
                // Verify OTP from the temporary store
                if (_otpStore.ContainsKey(phoneNumber) && _otpStore[phoneNumber] == otp)
                {
                    _otpStore.Remove(phoneNumber); // OTP used once, remove it
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
        }
}
