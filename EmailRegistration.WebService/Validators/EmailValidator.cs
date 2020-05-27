using EmailRegistration.Data.Entities;
using FluentValidation;
using System;

namespace EmailRegistration.WebService.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(Email => Email.EmailName).NotNull().NotEmpty();
            RuleFor(Email => Email.EmailRegistrationDate).NotNull().NotEmpty();
            RuleFor(Email => Email.EmailTo).NotNull().NotEmpty();
            RuleFor(Email => Email.EmailFrom).NotNull().NotEmpty();
            RuleFor(Email => Email.EmailTag).NotNull().NotEmpty();
            RuleFor(Email => Email.EmailContent).NotNull().NotEmpty();
        }

        public bool IsValidDatetime(string someval)
        {
            bool valid = false;
            DateTime testDate = DateTime.MinValue;
            DateTime minDateTime = DateTime.MaxValue;
            DateTime maxDateTime = DateTime.MinValue;

            minDateTime = new DateTime(2020, 1, 1);
            maxDateTime = new DateTime(2025, 12, 31, 23, 59, 59, 997);

            if (DateTime.TryParse(someval, out testDate))
            {
                if (testDate >= minDateTime && testDate <= maxDateTime)
                {
                    valid = true;
                }
            }
            return valid;
        }
    }
}