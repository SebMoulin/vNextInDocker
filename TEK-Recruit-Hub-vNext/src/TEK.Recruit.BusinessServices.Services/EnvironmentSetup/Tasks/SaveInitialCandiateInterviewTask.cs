using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.Commons.Entities.Interview;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    public class SaveInitialCandiateInterviewTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IHandleCandidateInterview _candidateInterviewService;
        private readonly string _city;
        private readonly string _country;
        private readonly string _postalCode;
        private readonly string _state;
        private readonly string _position;
        private readonly string _tekCenter;

        internal SaveInitialCandiateInterviewTask(IHandleCandidateInterview candidateInterviewService, 
            string city, 
            string state,
            string postalCode,
            string country, 
            string position, 
            string tekCenter)
        {
            if (candidateInterviewService == null) throw new ArgumentNullException("candidateInterviewService");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException("city");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentNullException("country");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentNullException("postalCode");
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentNullException("state");
            if (string.IsNullOrWhiteSpace(position)) throw new ArgumentNullException("position");
            if (string.IsNullOrWhiteSpace(tekCenter)) throw new ArgumentNullException("tekCenter");

            _candidateInterviewService = candidateInterviewService;
            _city = city;
            _country = country;
            _position = position;
            _tekCenter = tekCenter;
            _postalCode = postalCode;
            _state = state;
            CanRunInParallel = true;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var interview = new Interview()
            {
                CustomerId = token.CustomerId,
                CustomerName = token.CustomerName,
                CandidateProfile = new CandidateProfile()
                {
                    Address = new Address()
                    {
                        City = _city,
                        Country = _country,
                        PostalCode = _postalCode,
                        State = _state,
                    },
                    CandidateId = token.User.Id,
                    ProjectId = token.ProjectId,
                    DevEnv = token.DevEnv.ToString(),
                    Email = token.User.Email,
                    Name = token.User.Name,
                    Username = token.User.Username,
                    Position = _position,
                    TEKCenter = _tekCenter
                },
                FinalAssement = new FinalAssessment(),
                InterviewEyeball = new InterviewEyeball(),
                InterviewFeedback = new InterviewFeedback()
            };

            var success = await _candidateInterviewService.SaveCandidateInterview(interview);
            token.Success = success;
            token.Message = success ? "First interview saved" : "An error occured when saving first time interview";
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}