﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.SMTPRepo
{
    public interface ISMTPEmail
    {
        Task SentPasswordAsync(string toEmail, string subject, string body);
    }
}
