using System;
using System.Runtime.Serialization;

namespace EmailRegistration.Data.Entities
{
    public class Email
    {
        public int EmailId { get; set; }
        public string EmailName { get; set; }
        public DateTime EmailRegistrationDate { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTag { get; set; }
        public string EmailContent { get; set; }
        public bool Attachments { get; set; }
    }
}
