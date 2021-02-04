using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task<int> SendSystemNotificationAsync(string recipientUsername, string subject, string message, CancellationToken cancellationToken);
    }
}