using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task<int> SendSms(string recipientNumber, string title, string message, CancellationToken cancellationToken);
        Task<int> SendEmail(string recipientMail, string subject, string message, CancellationToken cancellationToken);
    }
}
