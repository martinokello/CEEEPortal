﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CEEEServices.Interfaces
{
    public interface IMailService
    {
        bool SendMail(MailMessage message);
    }
}
