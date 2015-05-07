using System.Linq;
using AutoMapper;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Web.ViewModels;

namespace TEK.Recruit.Web
{
    public static class AutoMapperBootstrap
    {
        public static void Configure()
        {
            Mapper.CreateMap<AddressViewModel, AddressDto>()
                .ReverseMap();
            Mapper.CreateMap<CandidateProfileViewModel, CandidateProfileDto>()
                .ReverseMap();
            Mapper.CreateMap<FinalAssessmentViewModel, FinalAssessmentDto>()
                .ForMember(dest => dest.PassedTheSelection, opt => opt.MapFrom(src => src.PassedFinalSelection))
                .ForMember(dest => dest.PassedTheSelectionDate, opt => opt.MapFrom(src => src.PassedFinalSelectionDate))
                .ReverseMap()
                .ForMember(dest => dest.PassedFinalSelection, opt => opt.MapFrom(src => src.PassedTheSelection))
                .ForMember(dest => dest.PassedFinalSelectionDate, opt => opt.MapFrom(src => src.PassedTheSelectionDate));
            Mapper.CreateMap<InterviewEyeballViewModel, InterviewEyeballDto>()
                .ForMember(dest => dest.PassedTheSelection, opt => opt.MapFrom(src => src.PassedFirstScreenSelection))
                .ForMember(dest => dest.PassedTheSelectionDate, opt => opt.MapFrom(src => src.PassedFirstScreenSelectionDate))
                .ReverseMap()
                .ForMember(dest => dest.PassedFirstScreenSelection, opt => opt.MapFrom(src => src.PassedTheSelection))
                .ForMember(dest => dest.PassedFirstScreenSelectionDate, opt => opt.MapFrom(src => src.PassedTheSelectionDate));
            Mapper.CreateMap<InterviewFeedbackViewModel, InterviewFeedbackDto>()
                .ReverseMap();

            Mapper.CreateMap<CandidateEvaluationFormViewModel, InterviewDto>()
                .AfterMap((vm, dto) =>
                {
                    if (vm.CandidateProfile == null)
                        vm.CandidateProfile = new CandidateProfileViewModel();
                    if (vm.FinalAssement == null)
                        vm.FinalAssement = new FinalAssessmentViewModel();
                    if (vm.InterviewEyeball == null)
                        vm.InterviewEyeball = new InterviewEyeballViewModel();
                    if (vm.InterviewFeedback == null)
                        vm.InterviewFeedback = new InterviewFeedbackViewModel();
                })
                .ReverseMap()
                .AfterMap((dto, vm) =>
                {
                    if (dto.CandidateProfile == null)
                        dto.CandidateProfile = new CandidateProfileDto();
                    if (dto.FinalAssement == null)
                        dto.FinalAssement = new FinalAssessmentDto();
                    if (dto.InterviewEyeball == null)
                        dto.InterviewEyeball = new InterviewEyeballDto();
                    if (dto.InterviewFeedback == null)
                        dto.InterviewFeedback = new InterviewFeedbackDto();
                });

            Mapper.CreateMap<CandidateEnvironmentDto, NewTestEnvironmentSetUpViewModel>()
                .ForMember(dest => dest.Message, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt => opt.Ignore())
                .ForMember(dest => dest.AvailableCenter, opt => opt.Ignore())
                .ForMember(dest => dest.AvailablePosition, opt => opt.Ignore())
                .ForMember(dest => dest.AvailableTestEnv, opt => opt.Ignore())
                .ForMember(dest => dest.Customers, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(dto => dto.CandidateName))
                .ReverseMap()
                .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(vm => vm.Name))
                .ForMember(dest => dest.TekCenter, opt => opt.MapFrom(vm => vm.SelectedCenter))
                .ForMember(dest => dest.DevEnv, opt => opt.MapFrom(vm => vm.SelectedDevEnv))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(vm => vm.SelectedCustomer))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(vm => vm.Customers.FirstOrDefault(c => c.Value == vm.SelectedCustomer).Text))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(vm => vm.SelectedPosition));
        }
    }
}

