using Application.Common.Configuration;
using Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NString;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger<NotificationService> _logger;
        private readonly IApplicationDbContext _context;

        public NotificationService(IEmailSender emailSender, ISmsSender smsSender, ILogger<NotificationService> logger, IApplicationDbContext context)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = logger;
            _context = context;
        }

        public async Task<int> SendSystemNotificationAsync(string recipientUsername, string subject, string message, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
        
        public async Task<int> SendNotificationAsync(string notificationSystemName, object recipientDetails, CancellationToken cancellationToken)
        {
            var template =
                await _context.NotificationTemplates.FirstOrDefaultAsync(x => x.SystemName == notificationSystemName, cancellationToken);
            if (null == template)
            {
                var e = new NotFoundException(nameof(template), notificationSystemName);
                _logger.LogError(e.Message);
                return 0;
            }

            // Get the important details 
            var userIdProperty = recipientDetails.GetType().GetProperty("UserId");
            if (null == userIdProperty)
            {
                var e = new NotFoundException(nameof(userIdProperty));
                _logger.LogError(e.Message);
                return 0;
            }

            var userEmailProperty = recipientDetails.GetType().GetProperty("Email");
            if (null == userEmailProperty)
            {
                var e = new NotFoundException(nameof(userEmailProperty));
                _logger.LogError(e.Message);
                return 0;
            }

            var userPhoneNumberProperty = recipientDetails.GetType().GetProperty("PhoneNumber");
            if (null == userPhoneNumberProperty)
            {
                var e = new NotFoundException(nameof(userPhoneNumberProperty));
                _logger.LogError(e.Message);
                return 0;
            }

            var userFullNameProperty = recipientDetails.GetType().GetProperty("FullName");
            if (null == userFullNameProperty)
            {
                var e = new NotFoundException(nameof(userFullNameProperty));
                _logger.LogError(e.Message);
                return 0;
            }



            StringTemplate.Format(template.SystemTemplate, recipientDetails);

            // Send notification to user
            var systemNotification = new SystemNotification()
            {

            };

            
            throw new NotImplementedException();
        }
        

    }
}
