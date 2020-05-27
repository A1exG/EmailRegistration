
using System.IO;
using System.Xml.Serialization;

namespace EmailRegistration.WebService.DbServices
{
    public class SettingsService : ISettingsService
    {
        public string ConnectionString { get; set; }
    }
}