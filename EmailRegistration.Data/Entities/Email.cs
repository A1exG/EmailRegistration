using System;

namespace EmailRegistration.Data.Entities
{
    public class Email : IEntity
    {
        public int Id { get; set; }
        public string EmailName { get; set; }
        public DateTime EmailRegistrationDate { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTag { get; set; }
        public string EmailContent { get; set; }
    }
}
