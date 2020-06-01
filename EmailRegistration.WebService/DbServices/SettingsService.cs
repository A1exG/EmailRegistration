using System.Configuration;

namespace EmailRegistration.WebService.DbServices
{
    public class SettingsService : ISettingsService
    {
        public string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["EmailContext"].ConnectionString;
        }
    }
}