using System;

namespace EmailRegistration.WebService.DbServices
{
    public class SettingsService : ISettingsService
    {
        public string ConnectionString { get; set; }
    }
}