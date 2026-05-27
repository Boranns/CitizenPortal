namespace CitizenPortal.Services;

public interface IEmailService
{
    Task SendApplicationStatusEmailAsync(string toEmail, string name, string status);
}
