using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISmsSender
    {
        Task<int> SendSmsAsync(string recipientNumber, string title, string message, CancellationToken cancellationToken);
    }
}
