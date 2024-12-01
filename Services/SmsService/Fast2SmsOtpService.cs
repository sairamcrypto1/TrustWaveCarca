//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace TrustWaveCarca.Services.SmsService
//{
//    public class Fast2SmsOtpService : IOtpService
//    {
//        private readonly string _apiKey;
//        private readonly HttpClient _httpClient;
//        private readonly Dictionary<string, string> _otpStore;

//        public Fast2SmsOtpService(IConfiguration configuration)
//        {
//            _apiKey = configuration["Fast2SMS:ApiKey"];
//            _httpClient = new HttpClient();
//            _otpStore = new Dictionary<string, string>();
//        }

//        public async Task<bool> SendOtpAsync(string phoneNumber, string otp)
//        {
//            try
//            {
//                // Prepare the request payload
//                var payload = new
//                {
//                    route = "q", // 'q' for transactional SMS
//                    sender_id = "TXTIND", // Default sender ID for transactional SMS
//                    message = $"Your OTP is: {otp}",
//                    language = "english",
//                    numbers = phoneNumber
//                };

//                var jsonPayload = JsonSerializer.Serialize(payload);
//                var request = new HttpRequestMessage(HttpMethod.Post, "https://www.fast2sms.com/dev/bulkV2")
//                {
//                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
//                };

//                // Add the authorization header
//                request.Headers.Add("authorization", _apiKey);

//                // Send the request
//                var response = await _httpClient.SendAsync(request);

//                // Check response status
//                if (response.IsSuccessStatusCode)
//                {
//                    _otpStore[phoneNumber] = otp; // Temporary store for OTPs
//                    return true;
//                }
//                else
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    Console.WriteLine($"Error sending OTP: {response.ReasonPhrase} - {errorContent}");
//                    return false;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception while sending OTP: {ex.Message}");
//                return false;
//            }
//        }

//        public Task<bool> VerifyOtpAsync(string phoneNumber, string otp)
//        {
//            if (_otpStore.ContainsKey(phoneNumber) && _otpStore[phoneNumber] == otp)
//            {
//                _otpStore.Remove(phoneNumber);
//                return Task.FromResult(true);
//            }
//            return Task.FromResult(false);
//        }
//    }
//}
