using Gyneco.Application.Models.Email;

namespace Gyneco.Domain.Contracts.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailMessage email);
    }
}
