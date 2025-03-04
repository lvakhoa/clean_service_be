namespace CleanService.Src.Modules.Mail.Services;

public interface IMailService
{
    public Task SendMail(string to, string subject, string body);
}