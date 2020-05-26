using EmailRegistration.Data.Entities;
using EmailRegistration.WebService.Repository;
using EmailRegistration.WebService.Validators;
using FluentValidation.Results;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace EmailRegistration.WebService.Services
{
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebService : System.Web.Services.WebService
    {
        private IKernel _kernel;
        private Repo _repo;
        private EmailValidator _validator;
        private Logger _logger;

        public WebService()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<Repo>().ToSelf();
            kernel.Bind<EmailValidator>().ToSelf();
            kernel.Bind<Logger>().ToSelf();

            var validator = kernel.Get<EmailValidator>();
            var repo = kernel.Get<Repo>();
            Logger logger = LogManager.GetCurrentClassLogger();

            _repo = repo;
            _validator = validator;
            _kernel = kernel;
            _logger = logger;
        }

        [WebMethod]
        public List<Email> Get()
        {
            List<Email> eList = _repo.Get();
            return eList;
        }

        [WebMethod]
        public Email GetByID(int id)
        {
            Email email = _repo.GetByID(id);
            return email;
        }

        [WebMethod]
        public void Insert(Email t)
        {
            ValidationResult result = _validator.Validate(t);
            if (result.IsValid)
            {
                _repo.Insert(t);
            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    _logger.Error("Property " + failure.PropertyName + " failed validation.Error was: " + failure.ErrorMessage);
                }
            } 
        }

        [WebMethod]
        public void Update(Email t)
        {
            ValidationResult result = _validator.Validate(t);
            if (result.IsValid)
            {
                _repo.Update(t);
            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    _logger.Error("Property " + failure.PropertyName + " failed validation.Error was: " + failure.ErrorMessage);
                }
            }   
        }

        [WebMethod]
        public List<Email> GetDateTimePeriod(DateTime start, DateTime end)
        {
            List<Email> eList = new List<Email>();
            if (start != null && end != null)
            {
                eList = _repo.GetDateTimePeriod(start, end);
                return eList;
            }
            return eList;
        }

        [WebMethod]
        public List<Email> GetByTo(string str)
        {
            List<Email> eList = new List<Email>();
            if(str != null)
            {
                eList = _repo.GetByTo(str);
                return eList;
            }
            return eList;
        }

        [WebMethod]
        public List<Email> GetByFrom(string str)
        {
            List<Email> eList = new List<Email>();
            if (str != null)
            {
                eList = _repo.GetByFrom(str);
                return eList;
            }
            return eList;
        }

        [WebMethod]
        public List<Email> GetByTag(string str)
        {
            List<Email> eList = new List<Email>();
            if (str != null)
            {
                eList = _repo.GetByTag(str);
                return eList;
            }
            return eList;
        }
    }
}
