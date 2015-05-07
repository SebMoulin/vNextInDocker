using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using TEK.Recruit.BusinessServices.Services;
using TEK.Recruit.BusinessServices.Services.EnvironmentSetup;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Facade;
using TEK.Recruit.Facade.Services;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http.Services;
using TEK.Recruit.Web;

namespace TEK.Recruit.SetUpCandidateRuntime
{
    public class vNextApplicationRuntime
    {
        private vNextApplicationRuntime(IServiceCollection services)
        {
            
        }
        public static vNextApplicationRuntime StartApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInstance<IConfiguration>(configuration);
            ConfigureFacadeDi(services);
            ConfigureBusinessDi(services);
            ConfigureRepositoriesDi(services);
            ConfigureFrameworkDi(services);
            AutoMapperBootstrap.Configure();
            AutoMapperConfiguration.Configure();
            return new vNextApplicationRuntime(services);
        }

        private static void ConfigureFacadeDi(IServiceCollection services)
        {
            services.AddTransient<ICodingExcerciseEnvironmentFacade, CodingExcerciseEnvironmentFacade>();
            services.AddTransient<IHandleMapping, MappingService>();
        }

        private static void ConfigureBusinessDi(IServiceCollection services)
        {
            services.AddTransient<IHandleAdmin, AdminService>();
            services.AddTransient<ISetUpCodingExcerciseEnvironment, CodingExcerciseEnvironmentSetUpService>();
            services.AddTransient<IManageCodingExcerciseEnvironment, CodingExcerciseEnvironmentService>();
            services.AddTransient<IHandleReporting, ReportingService>();
            services.AddTransient<IGenerateElasticSearchReports, ElasticSearchReportsGenerator>();
            services.AddTransient<IHandleCandidateInterview, CandidateInterviewService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICoordonateTasks<EnvironmentSetUpResult>, EnvironmentSetupCoordinator>();
        }

        private static void ConfigureRepositoriesDi(IServiceCollection services)
        {
            services.AddTransient<IGeolocator, Geolocator>();
            services.AddTransient<IGeoHash, GeoHasher>();
            services.AddTransient<IElasticSearhApi, ElasticSearhApi>();
            services.AddTransient<ISonarApi, SonarApi>();
            services.AddTransient<ISlackApi, SlackApi>();
            services.AddTransient<IGitLabApi, GitLabApi>();
        }
        
        private static void ConfigureFrameworkDi(IServiceCollection services)
        {
            services.AddTransient<IHandleHttpRequest, HttpRequestHandler>();
            services.AddTransient<IProvideConfig, ConfigProvider>();
        }
    }
}
