using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Commons.Extensions;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class AdminService :  IHandleAdmin
    {
        private readonly IProvideConfig _configProvider;
        private readonly IElasticSearhApi _elasticSearhApi;

        public AdminService(IProvideConfig configProvider, IElasticSearhApi elasticSearhApi)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (elasticSearhApi == null) throw new ArgumentNullException("elasticSearhApi");
            _configProvider = configProvider;
            _elasticSearhApi = elasticSearhApi;
        }

        public async Task<bool> DeleteRecruiterReportIndex(string customerId)
        {
            var reportIndex = _configProvider.GetElasticSearchRecruiterReportIndex();
            var customer = await _elasticSearhApi.GetCustomerbyId(customerId);
            var elIndex = _elasticSearhApi.GenerateCustomerRecruiterReportIndex(customer.Name.RemoveAllSpacesAnLower(), reportIndex);
            return await _elasticSearhApi.DeleteRecruiterReportIndex(elIndex);
        }

        public async Task<bool> CreateRecruiterReportMapping(string customerId)
        {
            var reportIndex = _configProvider.GetElasticSearchRecruiterReportIndex();
            var customer = await _elasticSearhApi.GetCustomerbyId(customerId);
            var elIndex = _elasticSearhApi.GenerateCustomerRecruiterReportIndex(customer.Name.RemoveAllSpacesAnLower(), reportIndex);
            var response = await _elasticSearhApi.CreateRecruiterReportMapping(elIndex);
            return response.SuccessfullyCreated;
        }

        public async Task<bool> CreateCustomer(string customerName)
        {
            var json = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = customerName
            };
            var response = await _elasticSearhApi.CreateCustomer(json);
            return response.SuccessfullyCreated;
        }

        public async Task<Customer[]> GetAllCustomers()
        {
            return await _elasticSearhApi.GetAllCustomers();
        }
    }
}
