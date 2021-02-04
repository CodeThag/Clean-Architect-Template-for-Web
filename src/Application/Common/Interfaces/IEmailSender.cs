﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipientMail, string subject, string message);
        Task SendEmailAsync(string recipientMail, string subject, string templateCode, string message);
    }
}