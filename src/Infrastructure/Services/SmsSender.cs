using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Configuration;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
   public class SmsSender : ISmsSender
    {
        private readonly SMSConfiguration _smsConfiguration;
        private readonly ILogger<SmsSender> _logger;

        public SmsSender(SMSConfiguration smsConfiguration, ILogger<SmsSender> logger)
        {
            _smsConfiguration = smsConfiguration;
            _logger = logger;
        }

        public async Task<int> SendSmsAsync(string recipientNumber, string title, string message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
