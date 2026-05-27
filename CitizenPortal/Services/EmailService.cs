using MailKit.Net.Smtp;
using MimeKit;

namespace CitizenPortal.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendApplicationStatusEmailAsync(string toEmail, string name, string status)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("CitizenPortal", _configuration["Email:From"]));
        message.To.Add(new MailboxAddress(name, toEmail));
        message.Subject = "Din ansøgning er blevet opdateret";

        message.Body = new TextPart("html")
        {
            Text = $@"
                <h2>Hej {name},</h2>
                <p>Din ansøgning er blevet opdateret.</p>
                <p>Ny status: <strong>{status}</strong></p>
                <br/>
                <p>Med venlig hilsen,</p>
                <p>CitizenPortal</p>"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            _configuration["Email:Host"],
            int.Parse(_configuration["Email:Port"]!),
            false);
        await client.AuthenticateAsync(
            _configuration["Email:Username"],
            _configuration["Email:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}