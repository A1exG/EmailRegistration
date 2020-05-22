using EmailRegistration.Data.Entities;
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

        private void connectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.IntegratedSecurity = true;
            connStringBuilder.InitialCatalog = "EmailDb";
            connStringBuilder.DataSource = @"DESKTOP-RH4I9L4\SQLEXPRESS";

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();
        }

        public int AddNewEmail(string emailName, DateTime emailRegistrationDate, string emailTo,
            string emailFrom, string emailTag, string emailContent, bool attachments)
        {
            try
            {
                comm.CommandText = "INSERT INTO Emails Values(@EmailName, @EmailRegistrationDate, @EmailTo, @EmailFrom, @EmailTag, @EmailContent, @Attachments)";
                comm.Parameters.AddWithValue("EmailName", emailName);
                comm.Parameters.AddWithValue("EmailRegistrationDate", emailRegistrationDate);
                comm.Parameters.AddWithValue("EmailTo", emailTo);
                comm.Parameters.AddWithValue("EmailFrom", emailFrom);
                comm.Parameters.AddWithValue("EmailTag", emailTag);
                comm.Parameters.AddWithValue("EmailContent", emailContent);
                comm.Parameters.AddWithValue("Attachments", attachments);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Не получилось!", ex);
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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Email GetEmailInId(int emailId)
        {
            Email email = new Email();
            try
            {
                comm.CommandText = "SELECT * FROM Users WHERE EmailId=@emailId";
                comm.Parameters.AddWithValue("emailId", emailId);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}