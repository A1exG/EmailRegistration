using EmailRegistration.Data.Entities;
using EmailRegistration.WebService.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace EmailRegistration.WebService.Services
{
    /// <summary>
    /// Сервис email
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {
        private SqlCommand sqlCom;

        /// <summary>
        /// Регистрация в системе нового входящего письма
        /// </summary>
        /// <param name="emailName">Название письма</param>
        /// <param name="emailRegistrationDate">Дата регистрации в системе</param>
        /// <param name="emailTo">Адресат письма</param>
        /// <param name="emailFrom">Отправитель письма</param>
        /// <param name="emailTag">Тэги</param>
        /// <param name="emailContent">Содержание письма</param>
        /// <param name="attachments">Вложения(есть/нет)</param>
        /// <returns></returns>
        [WebMethod]
        public int AddNewEmail(string emailName, DateTime emailRegistrationDate, string emailTo,
            string emailFrom, string emailTag, string emailContent, bool attachments)
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();

            try
            {
                sqlCom.CommandText = "INSERT INTO Emails Values(@EmailName, @EmailRegistrationDate, @EmailTo, @EmailFrom, @EmailTag, @EmailContent, @Attachments)";
                sqlCom.Parameters.AddWithValue("EmailName", emailName);
                sqlCom.Parameters.AddWithValue("EmailRegistrationDate", emailRegistrationDate);
                sqlCom.Parameters.AddWithValue("EmailTo", emailTo);
                sqlCom.Parameters.AddWithValue("EmailFrom", emailFrom);
                sqlCom.Parameters.AddWithValue("EmailTag", emailTag);
                sqlCom.Parameters.AddWithValue("EmailContent", emailContent);
                sqlCom.Parameters.AddWithValue("Attachments", attachments);

                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                return sqlCom.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom.Connection != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Получение списка всех писем
        /// </summary>
        /// <returns>Список всех писем</returns>
        [WebMethod]
        public List<Email> GetAllEmails()
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();
            List<Email> eventL = new List<Email>();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Emails";
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = Convert.ToInt32(reader[0]),
                        EmailName = reader[1].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[2]),
                        EmailTo = reader[3].ToString(),
                        EmailFrom = reader[4].ToString(),
                        EmailTag = reader[5].ToString(),
                        EmailContent = reader[6].ToString(),
                        Attachments = Convert.ToBoolean(reader[7])
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Получение письма по Id
        /// </summary>
        /// <param name="emailId">Id письма</param>
        /// <returns>Письмо</returns>
        [WebMethod]
        public Email GetEmailInId(int emailId)
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();

            Email email = new Email();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Users WHERE EmailId=@emailId";
                sqlCom.Parameters.AddWithValue("emailId", emailId);
                sqlCom.CommandType = CommandType.Text;

                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    email.EmailId = Convert.ToInt32(reader[0]); email.EmailName = reader[1].ToString();
                    email.EmailRegistrationDate = Convert.ToDateTime(reader[2]); email.EmailTo = reader[3].ToString();
                    email.EmailFrom = reader[4].ToString(); email.EmailTag = reader[5].ToString();
                    email.EmailContent = reader[6].ToString(); email.Attachments = Convert.ToBoolean(reader[7]);
                }
                return email;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
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
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();
            List<Email> eventL = new List<Email>();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Emails WHERE EmailRegistrationDate BETWEEN @start AND @end";
                sqlCom.Parameters.AddWithValue("start", start);
                sqlCom.Parameters.AddWithValue("end", end);
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = Convert.ToInt32(reader[0]),
                        EmailName = reader[1].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[2]),
                        EmailTo = reader[3].ToString(),
                        EmailFrom = reader[4].ToString(),
                        EmailTag = reader[5].ToString(),
                        EmailContent = reader[6].ToString(),
                        Attachments = Convert.ToBoolean(reader[7])
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по адресату
        /// </summary>
        /// <param name="emailTo">Адресат</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailTo(string emailTo)
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();
            List<Email> eventL = new List<Email>();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Emails WHERE EmailTo=@emailTo";
                sqlCom.Parameters.AddWithValue("emailTo", emailTo);
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = Convert.ToInt32(reader[0]),
                        EmailName = reader[1].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[2]),
                        EmailTo = reader[3].ToString(),
                        EmailFrom = reader[4].ToString(),
                        EmailTag = reader[5].ToString(),
                        EmailContent = reader[6].ToString(),
                        Attachments = Convert.ToBoolean(reader[7])
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по отправителю
        /// </summary>
        /// <param name="emailFrom">Отправитель</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailFrom(string emailFrom)
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();
            List<Email> eventL = new List<Email>();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Emails WHERE EmailFrom=@emailFrom";
                sqlCom.Parameters.AddWithValue("emailFrom", emailFrom);
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = Convert.ToInt32(reader[0]),
                        EmailName = reader[1].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[2]),
                        EmailTo = reader[3].ToString(),
                        EmailFrom = reader[4].ToString(),
                        EmailTag = reader[5].ToString(),
                        EmailContent = reader[6].ToString(),
                        Attachments = Convert.ToBoolean(reader[7])
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по тегу
        /// </summary>
        /// <param name="emailTag">Тэг</param>
        /// <returns>Список писем</returns>
        [WebMethod]
        public List<Email> GetEmailTag(string emailTag)
        {
            DataBaseAdoNet db = new DataBaseAdoNet();
            sqlCom = db.connectToDb();
            List<Email> eventL = new List<Email>();

            try
            {
                sqlCom.CommandText = "SELECT * FROM Emails WHERE EmailTag=@emailTag";
                sqlCom.Parameters.AddWithValue("emailTag", emailTag);
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Connection.Open();

                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = Convert.ToInt32(reader[0]),
                        EmailName = reader[1].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[2]),
                        EmailTo = reader[3].ToString(),
                        EmailFrom = reader[4].ToString(),
                        EmailTag = reader[5].ToString(),
                        EmailContent = reader[6].ToString(),
                        Attachments = Convert.ToBoolean(reader[7])
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
                throw error;
            }
            finally
            {
                if (sqlCom != null)
                {
                    sqlCom.Connection.Close();
                }
            }
        }
    }
}
