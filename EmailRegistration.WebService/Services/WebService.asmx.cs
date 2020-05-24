using EmailRegistration.Data.Entities;
using EmailRegistration.WebService.DataBase;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace EmailRegistration.WebService.Services
{
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {
        DataBaseAdoNet db = new DataBaseAdoNet();
        /// <summary>
        /// Регистрация в системе нового входящего письма
        /// </summary>
        /// <param name="emailName">Название письма</param>
        /// <param name="emailRegistrationDate">Дата регистрации в системе</param>
        /// <param name="emailTo">Адресат письма</param>
        /// <param name="emailFrom">Отправитель письма</param>
        /// <param name="emailTag">Тэги</param>
        /// <param name="emailContent">Содержание письма</param>
        /// <returns></returns>
        [WebMethod]
        public int AddNewEmail(string emailName, DateTime emailRegistrationDate, string emailTo,
            string emailFrom, string emailTag, string emailContent)
        {
            var ab = db.AddNewEmail(emailName, emailRegistrationDate, emailTo, emailFrom, emailTag, emailContent);
            return ab;
        }

        /// <summary>
        /// Получение списка всех писем
        /// </summary>
        /// <returns>Список всех писем</returns>
        [WebMethod]
        public List<Email> GetAllEmails()
        {
            List<Email> eList = db.GetAllEmails();
            return eList;
        }

        /// <summary>
        /// Получение письма по Id
        /// </summary>
        /// <param name="emailId">Id письма</param>
        /// <returns>Письмо</returns>
        [WebMethod]
        public Email GetEmailInId(int emailId)
        {
            Email email = db.GetEmailInId(emailId);
            return email;
        }

        /// <summary>
        /// Поиск сообщений по диапазону дат
        /// </summary>
        /// <param name="start">Начальная дата диапазона</param>
        /// <param name="end">Конечная дата диапазона</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailPeriodDate(DateTime start, DateTime end)
        {
            List<Email> eList = db.GetEmailPeriodDate(start, end);
            return eList;
        }

        /// <summary>
        /// Поиск по адресату
        /// </summary>
        /// <param name="emailTo">Адресат</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailTo(string emailTo)
        {
            List<Email> eList = db.GetEmailTo(emailTo);
            return eList;
        }

        /// <summary>
        /// Поиск по отправителю
        /// </summary>
        /// <param name="emailFrom">Отправитель</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailFrom(string emailFrom)
        {
            List<Email> eList = db.GetEmailFrom(emailFrom);
            return eList;
        }

        /// <summary>
        /// Поиск по тегу
        /// </summary>
        /// <param name="emailTag">Тэг</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailTag(string emailTag)
        {
            List<Email> eList = db.GetEmailTag(emailTag);
            return eList;
        }
    }
}
