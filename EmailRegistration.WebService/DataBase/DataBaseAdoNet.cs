using EmailRegistration.Data.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmailRegistration.WebService.DataBase
{
    public class DataBaseAdoNet : IDb
    {
        public DataBaseAdoNet()
        {
            connectToDb();
        }

        SqlConnection conn;
        SqlCommand comm;
        SqlConnectionStringBuilder connStringBuilder;

        Logger logger = LogManager.GetCurrentClassLogger();


        private void connectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.IntegratedSecurity = true;
            connStringBuilder.InitialCatalog = "EmailDb";
            connStringBuilder.DataSource = @"DESKTOP-RH4I9L4\SQLEXPRESS";

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();
        }

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
        public int AddNewEmail(string emailName, DateTime emailRegistrationDate, string emailTo,
            string emailFrom, string emailTag, string emailContent)
        {
            try
            {
                comm.CommandText = "INSERT INTO Emails Values(@EmailName, @EmailRegistrationDate, @EmailTo, @EmailFrom, @EmailTag, @EmailContent)";
                comm.Parameters.AddWithValue("EmailName", emailName);
                comm.Parameters.AddWithValue("EmailRegistrationDate", emailRegistrationDate);
                comm.Parameters.AddWithValue("EmailTo", emailTo);
                comm.Parameters.AddWithValue("EmailFrom", emailFrom);
                comm.Parameters.AddWithValue("EmailTag", emailTag);
                comm.Parameters.AddWithValue("EmailContent", emailContent);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Получение списка всех писем
        /// </summary>
        /// <returns>Список всех писем</returns>
        public List<Email> GetAllEmails()
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails";
                comm.CommandType = CommandType.Text;
                comm.Connection.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                        EmailContent = reader[6].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Получение письма по Id
        /// </summary>
        /// <param name="emailId">Id письма</param>
        /// <returns>Письмо</returns>
        public Email GetEmailInId(int emailId)
        {
            Email email = new Email();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailId=@emailId";
                comm.Parameters.AddWithValue("emailId", emailId);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    email.EmailId = Convert.ToInt32(reader[0]); email.EmailName = reader[1].ToString();
                    email.EmailRegistrationDate = Convert.ToDateTime(reader[2]); email.EmailTo = reader[3].ToString();
                    email.EmailFrom = reader[4].ToString(); email.EmailTag = reader[5].ToString();
                    email.EmailContent = reader[6].ToString();
                }
                return email;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Поиск сообщений по диапазону дат
        /// </summary>
        /// <param name="start">Начальная дата диапазона</param>
        /// <param name="end">Конечная дата диапазона</param>
        /// <returns>Список писем</returns>
        public List<Email> GetEmailPeriodDate(DateTime start, DateTime end)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailRegistrationDate BETWEEN @start AND @end";
                comm.Parameters.AddWithValue("start", start);
                comm.Parameters.AddWithValue("end", end);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                        EmailContent = reader[6].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по адресату
        /// </summary>
        /// <param name="emailTo">Адресат</param>
        /// <returns>Список писем</returns>
        public List<Email> GetEmailTo(string emailTo)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailTo=@emailTo";
                comm.Parameters.AddWithValue("emailTo", emailTo);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                        EmailContent = reader[6].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по отправителю
        /// </summary>
        /// <param name="emailFrom">Отправитель</param>
        /// <returns>Список писем</returns>
        public List<Email> GetEmailFrom(string emailFrom)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailFrom=@emailFrom";
                comm.Parameters.AddWithValue("emailFrom", emailFrom);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                        EmailContent = reader[6].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Поиск по тегу
        /// </summary>
        /// <param name="emailTag">Тэг</param>
        /// <returns>Список писем</returns>
        public List<Email> GetEmailTag(string emailTag)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailTag=@emailTag";
                comm.Parameters.AddWithValue("emailTag", emailTag);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                        EmailContent = reader[6].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception("Не получилось!");
                throw error;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}