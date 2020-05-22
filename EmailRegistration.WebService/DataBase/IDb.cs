﻿using EmailRegistration.Data.Entities;
using System;
using System.Collections.Generic;

namespace EmailRegistration.WebService.DataBase
{
    public interface IDb
    {
        int AddNewEmail(string emailName, DateTime emailRegistrationDate, string emailTo,
            string emailFrom, string emailTag, string emailContent, bool attachments);
        List<Email> GetAllEmails();
        Email GetEmailInId(int emailId);
        List<Email> GetEmailPeriodDate(DateTime start, DateTime end);
        List<Email> GetEmailTo(string emailTo);
        List<Email> GetEmailFrom(string emailFrom);
        List<Email> GetEmailTag(string emailTag);

    }
}
