using EmailRegistration.Data.Entities;
using EmailRegistration.WebService.DbServices;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmailRegistration.WebService.Repository
{
    public class Repo : IRepository<Email>
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        Logger logger = LogManager.GetCurrentClassLogger();

        private const int EmailIdColumn = 0;
        private const int EmailNameColumn = 1;
        private const int EmailRegistrationDateColumn = 2;
        private const int EmailToColumn = 3;
        private const int EmailFromColumn = 4;
        private const int EmailTagColumn = 5;
        private const int EmailContentColumn = 6;

        public Repo()
        {
            connectToDb();
        }
        private void connectToDb()
        {
            SettingsService settingService = new SettingsService();
            var connectionString = settingService.ConnectionString();

            _sqlConnection = new SqlConnection(connectionString);
            _sqlCommand = _sqlConnection.CreateCommand();
        }

        public List<Email> Get()
        {
            List<Email> eventL = new List<Email>();
            try
            {
                string sqlExpression = "sp_GetEmails";
                _sqlCommand.CommandText = sqlExpression;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Connection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        Id = Convert.ToInt32(reader[EmailIdColumn]),
                        EmailName = reader[EmailNameColumn].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]),
                        EmailTo = reader[EmailToColumn].ToString(),
                        EmailFrom = reader[EmailFromColumn].ToString(),
                        EmailTag = reader[EmailTagColumn].ToString(),
                        EmailContent = reader[EmailContentColumn].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public Email GetByID(int id)
        {
            Email email = new Email();
            try
            {
                _sqlCommand.CommandText = "SELECT EmailId, EmailName, EmailRegistrationDate, EmailTo, EmailFrom, EmailTag, EmailContent FROM Emails WHERE EmailId=@id";
                _sqlCommand.Parameters.AddWithValue("id", id);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    email.Id = Convert.ToInt32(reader[EmailIdColumn]);
                    email.EmailName = reader[EmailNameColumn].ToString();
                    email.EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]);
                    email.EmailTo = reader[EmailToColumn].ToString();
                    email.EmailFrom = reader[EmailFromColumn].ToString();
                    email.EmailTag = reader[EmailTagColumn].ToString();
                    email.EmailContent = reader[EmailContentColumn].ToString();
                }
                return email;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public int Insert(Email t)
        {
            try
            {
                _sqlCommand.CommandText = "INSERT INTO Emails Values (@EmailName, @EmailRegistrationDate, @EmailTo, @EmailFrom, @EmailTag, @EmailContent)";
                _sqlCommand.Parameters.AddWithValue("EmailName", t.EmailName);
                _sqlCommand.Parameters.AddWithValue("EmailRegistrationDate", t.EmailRegistrationDate);
                _sqlCommand.Parameters.AddWithValue("EmailTo", t.EmailTo);
                _sqlCommand.Parameters.AddWithValue("EmailFrom", t.EmailFrom);
                _sqlCommand.Parameters.AddWithValue("EmailTag", t.EmailTag);
                _sqlCommand.Parameters.AddWithValue("EmailContent", t.EmailContent);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                return _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public int Update(Email t)
        {
            try
            {
                _sqlCommand.CommandText = "UPDATE Emails SET EmailName=@EmailName, EmailRegistrationDate=@EmailRegistrationDate, EmailTo=@EmailTo, EmailFrom=@EmailFrom, EmailTag=@EmailTag, EmailContent=@EmailContent WHERE EmailId=@EmailId";
                _sqlCommand.Parameters.AddWithValue("EmailId", t.Id);
                _sqlCommand.Parameters.AddWithValue("EmailName", t.EmailName);
                _sqlCommand.Parameters.AddWithValue("EmailRegistrationDate", t.EmailRegistrationDate);
                _sqlCommand.Parameters.AddWithValue("EmailTo", t.EmailTo);
                _sqlCommand.Parameters.AddWithValue("EmailFrom", t.EmailFrom);
                _sqlCommand.Parameters.AddWithValue("EmailTag", t.EmailTag);
                _sqlCommand.Parameters.AddWithValue("EmailContent", t.EmailContent);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                return _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Email> GetDateTimePeriod(DateTime start, DateTime end)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                _sqlCommand.CommandText = "SELECT EmailId, EmailName, EmailRegistrationDate, EmailTo, EmailFrom, EmailTag, EmailContent FROM Emails WHERE EmailRegistrationDate BETWEEN @start AND @end";
                _sqlCommand.Parameters.AddWithValue("start", start);
                _sqlCommand.Parameters.AddWithValue("end", end);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        Id = Convert.ToInt32(reader[EmailIdColumn]),
                        EmailName = reader[EmailNameColumn].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]),
                        EmailTo = reader[EmailToColumn].ToString(),
                        EmailFrom = reader[EmailFromColumn].ToString(),
                        EmailTag = reader[EmailTagColumn].ToString(),
                        EmailContent = reader[EmailContentColumn].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Email> GetByTo(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                _sqlCommand.CommandText = "SELECT EmailId, EmailName, EmailRegistrationDate, EmailTo, EmailFrom, EmailTag, EmailContent FROM Emails WHERE EmailTo=@str";
                _sqlCommand.Parameters.AddWithValue("str", str);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        Id = Convert.ToInt32(reader[EmailIdColumn]),
                        EmailName = reader[EmailNameColumn].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]),
                        EmailTo = reader[EmailToColumn].ToString(),
                        EmailFrom = reader[EmailFromColumn].ToString(),
                        EmailTag = reader[EmailTagColumn].ToString(),
                        EmailContent = reader[EmailContentColumn].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Email> GetByFrom(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                _sqlCommand.CommandText = "SELECT EmailId, EmailName, EmailRegistrationDate, EmailTo, EmailFrom, EmailTag, EmailContent FROM Emails WHERE EmailFrom=@str";
                _sqlCommand.Parameters.AddWithValue("str", str);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        Id = Convert.ToInt32(reader[EmailIdColumn]),
                        EmailName = reader[EmailNameColumn].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]),
                        EmailTo = reader[EmailToColumn].ToString(),
                        EmailFrom = reader[EmailFromColumn].ToString(),
                        EmailTag = reader[EmailTagColumn].ToString(),
                        EmailContent = reader[EmailContentColumn].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }

        public List<Email> GetByTag(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                _sqlCommand.CommandText = "SELECT EmailId, EmailName, EmailRegistrationDate, EmailTo, EmailFrom, EmailTag, EmailContent FROM Emails WHERE EmailTag=@str";
                _sqlCommand.Parameters.AddWithValue("str", str);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlConnection.Open();

                SqlDataReader reader = _sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        Id = Convert.ToInt32(reader[EmailIdColumn]),
                        EmailName = reader[EmailNameColumn].ToString(),
                        EmailRegistrationDate = Convert.ToDateTime(reader[EmailRegistrationDateColumn]),
                        EmailTo = reader[EmailToColumn].ToString(),
                        EmailFrom = reader[EmailFromColumn].ToString(),
                        EmailTag = reader[EmailTagColumn].ToString(),
                        EmailContent = reader[EmailContentColumn].ToString()
                    };
                    eventL.Add(email);
                }
                return eventL;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                Exception error = new Exception($"Ошибка. {ex}");
                throw error;
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}