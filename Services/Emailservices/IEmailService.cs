﻿namespace TrustWaveCarca.Services.Emailservices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
