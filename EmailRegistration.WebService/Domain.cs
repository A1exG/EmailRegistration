using EmailRegistration.WebService.DbServices;
using EmailRegistration.WebService.Repository;
using EmailRegistration.WebService.Validators;
using Ninject;
using NLog;

namespace EmailRegistration.WebService
{
    public class Domain
    {
        private IKernel _kernel;
        private Repo _repo;
        private EmailValidator _validator;
        private Logger _logger;
        public Domain()
        {
            IKernel kernel = new StandardKernel();
            _kernel = kernel;

            Init();

            var validator = kernel.Get<EmailValidator>();
            var repo = kernel.Get<Repo>();
            Logger logger = LogManager.GetCurrentClassLogger();

            _repo = repo;
            _validator = validator;
            _logger = logger; 
        }

        private Domain Init()
        {
            _kernel.Bind<Repo>().ToSelf();
            _kernel.Bind<EmailValidator>().ToSelf();
            _kernel.Bind<Logger>().ToSelf();
            _kernel.Bind<ISettingsService>().To<SettingsService>().InSingletonScope();

            return this;
        }

        public IKernel InitDependence()
        {
            return _kernel;
        }

        public Repo InitRepo()
        {
            return _repo;
        }

        public EmailValidator InitValidator()
        {
            return _validator;
        }

        public Logger InitLogger()
        {
            return _logger;
        }
    }
}