namespace TrustWaveCarca.Services.SmsService
{
    public interface IOtpService
    {
        Task<bool> SendOtpAsync(string phoneNumber, string otp);
        Task<bool> VerifyOtpAsync(string phoneNumber, string otp);
    }
}
