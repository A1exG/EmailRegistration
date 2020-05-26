using EmailRegistration.Data.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmailRegistration.WebService.Repository
{
    public class Repo : IRepository<Email>
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlConnectionStringBuilder connStringBuilder;

        Logger logger = LogManager.GetCurrentClassLogger();

        public Repo()
        {
            connectToDb();
        }
        private void connectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.IntegratedSecurity = true;
            connStringBuilder.InitialCatalog = "EmailDb";
            connStringBuilder.DataSource = @"DESKTOP-RH4I9L4\SQLEXPRESS";

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();
        }

        public List<Email> Get()
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

        public Email GetByID(int id)
        {
            Email email = new Email();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailId=@id";
                comm.Parameters.AddWithValue("id", id);
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

        public int Insert(Email t)
        {
            try
            {
                comm.CommandText = "INSERT INTO Emails Values (@EmailName, @EmailRegistrationDate, @EmailTo, @EmailFrom, @EmailTag, @EmailContent)";
                comm.Parameters.AddWithValue("EmailName", t.EmailName);
                comm.Parameters.AddWithValue("EmailRegistrationDate", t.EmailRegistrationDate);
                comm.Parameters.AddWithValue("EmailTo", t.EmailTo);
                comm.Parameters.AddWithValue("EmailFrom", t.EmailFrom);
                comm.Parameters.AddWithValue("EmailTag", t.EmailTag);
                comm.Parameters.AddWithValue("EmailContent", t.EmailContent);
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

        public int Update(Email t)
        {
            try
            {
                comm.CommandText = "UPDATE Emails SET EmailName=@EmailName, EmailRegistrationDate=@EmailRegistrationDate, EmailTo=@EmailTo, EmailFrom=@EmailFrom, EmailTag=@EmailTag, EmailContent=@EmailContent WHERE EmailId=@EmailId";
                comm.Parameters.AddWithValue("EmailId", t.EmailId);
                comm.Parameters.AddWithValue("EmailName", t.EmailName);
                comm.Parameters.AddWithValue("EmailRegistrationDate", t.EmailRegistrationDate);
                comm.Parameters.AddWithValue("EmailTo", t.EmailTo);
                comm.Parameters.AddWithValue("EmailFrom", t.EmailFrom);
                comm.Parameters.AddWithValue("EmailTag", t.EmailTag);
                comm.Parameters.AddWithValue("EmailContent", t.EmailContent);
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

        public List<Email> GetDateTimePeriod(DateTime start, DateTime end)
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

        public List<Email> GetByTo(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailTo=@str";
                comm.Parameters.AddWithValue("str", str);
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

        public List<Email> GetByFrom(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailFrom=@str";
                comm.Parameters.AddWithValue("str", str);
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

        public List<Email> GetByTag(string str)
        {
            List<Email> eventL = new List<Email>();
            try
            {
                comm.CommandText = "SELECT * FROM Emails WHERE EmailTag=@str";
                comm.Parameters.AddWithValue("str", str);
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