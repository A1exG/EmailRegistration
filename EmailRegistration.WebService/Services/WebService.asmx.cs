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
            Domain domain = new Domain();
            var kernel = domain.InitDependence();
 
            var validator = domain.InitValidator();
            var repo = domain.InitRepo();
            var logger = domain.InitLogger();

            _repo = repo;
            _validator = validator;
            _kernel = kernel;
            _logger = logger;
        }

        [WebMethod]
        public List<Email> Get()
        {
            List<Email> emailList = _repo.Get();
            return emailList;
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
                var a = _repo.Insert(t);
            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    _logger.Error($"Property - {failure.PropertyName} failed validation.Error was: {failure.ErrorMessage}");
                }
            } 
        }

        [WebMethod]
        public void Update(Email t)
        {
            ValidationResult result = _validator.Validate(t);
            if (result.IsValid)
            {
                var a = _repo.Update(t);
            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    _logger.Error($"Property - {failure.PropertyName} failed validation.Error was: {failure.ErrorMessage}");
                }
            }   
        }

        [WebMethod]
        public List<Email> GetDateTimePeriod(DateTime start, DateTime end)
        {
            List<Email> emailList = new List<Email>();
            
            if (_validator.IsValidSqlDatetime(start.ToString()) && _validator.IsValidSqlDatetime(end.ToString()))
            {
                emailList = _repo.GetDateTimePeriod(start, end);
                return emailList;
            }
            return emailList;
        }

        [WebMethod]
        public List<Email> GetByTo(string str)
        {
            List<Email> emailList = new List<Email>();
            if(str != null)
            {
                emailList = _repo.GetByTo(str);
                return emailList;
            }
            return emailList;
        }

        [WebMethod]
        public List<Email> GetByFrom(string str)
        {
            List<Email> emailList = new List<Email>();
            if (str != null)
            {
                emailList = _repo.GetByFrom(str);
                return emailList;
            }
            return emailList;
        }

        [WebMethod]
        public List<Email> GetByTag(string str)
        {
            List<Email> emailList = new List<Email>();
            if (str != null)
            {
                emailList = _repo.GetByTag(str);
                return emailList;
            }
            return emailList;
        }
    }
}
