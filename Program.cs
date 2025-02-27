using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TrustWaveCarca.Components;
using TrustWaveCarca.Components.Account;
using TrustWaveCarca.Data;
using TrustWaveCarca.Services.Emailservices;
using FluentEmail.Smtp;
using TrustWaveCarca.Services.SmsService;

namespace TrustWaveCarca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IOtpService, TwilioOtpService>();


            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
            
            builder.Services.AddServerSideBlazor()
            .AddCircuitOptions(options => { options.DetailedErrors = true; });


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => 
            options.SignIn.RequireConfirmedAccount = true)
           .AddRoles<IdentityRole>() // Ensure roles are added

                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


            // Configure email settings
            builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));

            // Register FluentEmail services
            var emailConfig = builder.Configuration.GetSection("EmailSettings").Get<EmailSetting>();

            builder.Services
                .AddFluentEmail(emailConfig.SenderEmail)
                .AddSmtpSender(new SmtpClient(emailConfig.SmtpServer)
                {
                    Port = emailConfig.SmtpPort,
                    Credentials = new System.Net.NetworkCredential(emailConfig.SmtpUser, emailConfig.SmtpPass),
                    EnableSsl = emailConfig.EnableSsl
                });

            // Register email service
            builder.Services.AddTransient<IEmailService, EmailService>();

            // Register IdentityEmailSender
            builder.Services.AddTransient<IdentityEmailSender>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
