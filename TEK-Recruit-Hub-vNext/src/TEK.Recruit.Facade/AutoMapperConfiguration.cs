using AutoMapper;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.Commons.Entities.Interview;

namespace TEK.Recruit.Facade
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<InterviewDto, Interview>()
                .ReverseMap();
            Mapper.CreateMap<AddressDto, Address>()
                .ReverseMap();
            Mapper.CreateMap<CandidateProfileDto, CandidateProfile>()
                .ReverseMap();
            Mapper.CreateMap<FinalAssessmentDto, FinalAssessment>()
                .ReverseMap();
            Mapper.CreateMap<InterviewEyeballDto, InterviewEyeball>()
                .ReverseMap();
            Mapper.CreateMap<InterviewFeedbackDto, InterviewFeedback>()
                .ReverseMap();
            Mapper.CreateMap<CustomerDto, Customer>()
                .ReverseMap();
            Mapper.CreateMap<CandidateEnvironmentDto, CandidateEnvironment>()
                .ReverseMap();
            Mapper.CreateMap<EnvironmentSetUpResultDto, EnvironmentSetUpResult>()
                .ReverseMap();
        }
    }
}
