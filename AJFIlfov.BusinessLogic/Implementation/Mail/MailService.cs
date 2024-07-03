using AJFIlfov.BusinessLogic;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Mail.Models;
using AJFIlfov.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

public class MailService : BaseService
{
    private readonly SmtpSettings _smtpSettings;

    public MailService(ServiceDependencies serviceDependencies, IConfiguration configuration) : base(serviceDependencies)
    {

        this._smtpSettings = new SmtpSettings();

        _smtpSettings.Username = configuration["SmtpSettings:Username"];
        _smtpSettings.Server = configuration["SmtpSettings:Server"];
        _smtpSettings.Password = configuration["SmtpSettings:Password"];
        _smtpSettings.Port = int.Parse(configuration["SmtpSettings:Port"]);
        //tryparse
    }

    public void SendNotificationEmail(string recipientEmail, string subject, string body)
    {
        using (var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress("eduard.cristea@essensys.ro", "AJF Ilfov Support"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipientEmail);

            smtpClient.Send(mailMessage);
        }
    }

    public string GetMail(Guid id)
    {
        var user = UnitOfWork.Users.Find(id);
        return user.Mail;
    }
}
