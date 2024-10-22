using Resend;

namespace CleanService.Src.Modules.Mail.Services;

public class ResendService : IMailService
{
    private readonly IResend _resend;
    
    private readonly IConfiguration _config;
    
    public ResendService(IResend resend, IConfiguration config)
    {
        _resend = resend;
        
        _config = config;
    }
    
    public async Task SendMail(string to, string subject, string body)
    {
        var message = new EmailMessage
        {
            From = _config.GetValue<string>("Mail:From")!,
            To = to,
            Subject = subject,
            HtmlBody = body
        };

        await _resend.EmailSendAsync( message );
    }
}