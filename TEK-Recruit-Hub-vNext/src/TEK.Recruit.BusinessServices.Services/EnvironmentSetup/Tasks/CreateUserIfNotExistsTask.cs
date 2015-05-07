using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class CreateUserIfNotExistsTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;
        private readonly string _email;
        private readonly string _name;
        private readonly string _customerId;
        private readonly string _customerName;
        private readonly string _username;

        public CreateUserIfNotExistsTask(IGitLabApi gitLabApi,
            string customerId,
            string customerName,
            string email,
            string name,
            string username)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");
            if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentNullException("customerId");
            if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentNullException("customerName");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException("username");
            _gitLabApi = gitLabApi;
            _email = email;
            _name = name;
            _customerId = customerId;
            _customerName = customerName;
            _username = username;
            CanRunInParallel = false;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var result = await UserExists(_email, token);
            token.CustomerId = _customerId;
            token.CustomerName = _customerName;
            if (result.User == null)
            {
                token = await CreateUser(token, _name, _email, _username);
            }
            return token;
        }
        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.User != null && token.Success;
        }

        private async Task<EnvironmentSetUpResult> UserExists(string email, EnvironmentSetUpResult token)
        {
            var users = await _gitLabApi.GetUsers(token.AdminToken);
            var matchingUser = users.FirstOrDefault(u => u.Email == email);
            if (matchingUser == null) return token;
            token.User = matchingUser;
            token.Success = true;
            token.Message = "User found in gitlab";
            return token;
        }
        private async Task<EnvironmentSetUpResult> CreateUser(EnvironmentSetUpResult token, string name, string email, string username)
        {
            var password = GeneratePassword(8);
            var response = await _gitLabApi.CreateUser(name, email, username, password, token.AdminToken);
            token.User = response.ReturnedObject;
            token.Success = response.SuccessfullyCreated;
            token.Message = response.SuccessfullyCreated ? "User created" : "An error occured while creating the user";
            token.User.Password = password;
            return token;
        }

        private string GeneratePassword(int length)
        {
            const string valid =
                "ABCDEFGHJKMNPQRSTUVWXYZ"
                + "abcdefghjkmnpqrstuvwxyz"
                + "23456789";
                //+ "_-@!";
            var password = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                password.Append(valid[rnd.Next(valid.Length)]);
            }
            return password.ToString();
        }
    }
}