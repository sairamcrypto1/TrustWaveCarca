using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System;
using TrustWaveCarca.Data;
using Microsoft.AspNetCore.Identity;
namespace TrustWaveCarca.Services.Emailservices
{
    public class IdentityEmailSender : IEmailSender
    {
        private readonly IEmailService _emailService;

        public IdentityEmailSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return _emailService.SendEmailAsync(email, subject, htmlMessage);
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var subject = "Confirm your email";
            //var message = $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>";
            var message = $@"
<div style='width: 100%; background-color: #f9f9f9; padding: 20px; box-sizing: border-box; font-family: Arial, sans-serif;'>
    <div style='max-width: 500px; margin: 0 auto; background-color: #ffffff; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
        <div style='text-align: center; margin-bottom: 20px;'>
            <img src='https://i.ibb.co/sCdmWTx/mainlogo.jpg' alt='Company Logo' style='width: 150px;'>
        </div>
        <h2 style='color: #333333; text-align: center; margin-bottom: 20px;'>Confirm Your Email Address</h2>
        <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 10px 0;'>
            Hello {user.UserName},
        </p>
        <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 20px 0;'>
            Thank you for registering with TrustWaveCarcaa. To complete your registration, please confirm your account by clicking the button below.
        </p>
        <div style='text-align: center; margin: 20px 0;'>
            <a href='{confirmationLink}' style='background-color: #005A80; color: #ffffff; padding: 8px 16px; text-decoration: none; border-radius: 5px; font-size: 16px;'>
                Confirm Email
            </a> 
        </div>
        <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 10px 0;'>
            If you did not create an account with us, please ignore this email. If you have any questions, feel free to contact our support team.
        </p>
        <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0;'>
            Best regards,<br>TrustWaveCarcaa
        </p>
    </div>
</div>";
            return SendEmailAsync(email, subject, message);
        }

        public Task SendPasswordResetCodeAsync(string email, string resetCode)
        {
            var subject = "Reset your password";
            var message = $"Your password reset code is: {resetCode}";
            return SendEmailAsync(email, subject, message);
        }

        public Task SendPasswordResetLinkAsync(string email, string resetLink)
        {
            var subject = "Reset your password";
            var message = $"Please reset your password by clicking this link: <a href='{resetLink}'>link</a>";
            return SendEmailAsync(email, subject, message);
        }
        public Task SendConfirmAckUniqueLoginIdAsync(ApplicationUser user, string email, string uniqueId)
        {
            var subject = "Registration Successful - Your Quick Login ID";
            var message = $@"
        <div style='width: 100%; background-color: #f9f9f9; padding: 20px; box-sizing: border-box; font-family: Arial, sans-serif;'>
            <div style='max-width: 500px; margin: 0 auto; background-color: #ffffff; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                <div style='text-align: center; margin-bottom: 20px;'>
                    <img src='https://i.ibb.co/sCdmWTx/mainlogo.jpg' alt='Company Logo' style='width: 150px;'>
                </div>
                <h2 style='color: #333333; text-align: center; margin-bottom: 20px;'>Registration Successful</h2>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 10px 0;'>
                    Hello {user.UserName},
                </p>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 20px 0;'>
                    Congratulations! Your registration with TrustWaveCarcaa was successful. To log in quickly to your account, use the unique ID below:
                </p>
                <div style='text-align: center; margin: 20px 0;'>
                    <p style='font-size: 18px; font-weight: bold; color: #005A80;'>Your Unique Login ID: {uniqueId.ToUpper()}</p>
                </div>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 10px 0;'>
                    You can use this ID for a quick login to your account in the future.
                </p>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0 0 20px 0;'>
                    If you did not register with us, please ignore this email. If you have any questions, feel free to contact our support team.
                </p>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; text-align: center; margin: 0;'>
                    Best regards,<br>TrustWaveCarcaa
                </p>
            </div>
        </div>";

            return SendEmailAsync(email, subject, message);
        }


        //public Task SendEmailDataSubmitAsync(string email, string subject, Student student)
        //{
        //    var message = $"Name: {student.Name}Email: {student.Email}Phone: {student.Phone}";
        //    return SendEmailAsync(email, subject, message);
        //}

        //public async Task SendEmailDataSubmitAsync(string email, string subject, Student student)
        //{
        //    var message = $@"
        //    <html>
        //    <head>
        //        <link href='https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css' rel='stylesheet'>
        //        <style>
        //            .container {{
        //                max-width: 600px;
        //                margin: 0 auto;
        //                padding: 20px;
        //                background-color: #ffffff;
        //                border: 1px solid #dddddd;
        //                border-radius: 5px;
        //                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        //            }}
        //            .header {{
        //                background-color: #005A80;
        //                color: #ffffff;
        //                padding: 10px 20px;
        //                text-align: center;
        //                border-radius: 5px 5px 0 0;
        //            }}
        //            .footer {{
        //                margin-top: 20px;
        //                text-align: center;
        //                color: #777;
        //            }}
        //        </style>
        //    </head>
        //    <body>
        //        <div class='container'>
        //            <div class='header'>
        //                <h2>Student Data Submission</h2>
        //            </div>
        //            <div class='content'>
        //                <p class='mt-3'>Hello,</p>
        //                <p>The following student data has been submitted:</p>
        //                <p>
        //                    <strong>Name:</strong> {student.Name} <br>
        //                    <strong>Email:</strong> {student.Email} <br>
        //                    <strong>Phone:</strong> {student.Phone}
        //                </p>
        //                <p>Thank you!</p>
        //            </div>
        //            <div class='footer'>
        //            <a href=""Sairam@support.com"">Support To Contact</a>
        //             <p>+91 703670373637</p>
        //            </div>
        //        </div>
        //    </body>
        //    </html>";

        //    await SendEmailAsync(email, subject, message);
        //}


        public Task SendEmailUpdateSubmitAsync(string email, string subject, string message)
        {
            return SendEmailAsync(email, subject, message);
        }
    }
}
