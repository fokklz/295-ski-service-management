using AutoMapper;
using SkiServiceAPI.DTOs;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using SkiServiceAPI.Services;

namespace SkiServiceAPI.Common
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configuration for AutoMapper
        /// Ensure that all DTOs are mapped to the corresponding Model and vice versa
        /// Also taking care of ignoring null values or ids on update or create
        /// </summary>
        public MappingProfile()
        {
            //########################################################
            //
            // Responses
            //
            //########################################################

            CreateMap<User, UserResponse>();
            CreateMap<Service, Service>();
            CreateMap<State, State>();
            CreateMap<Priority, Priority>();

            //########################################################
            //
            // REQUESTS
            //
            //########################################################

            CreateMap<CreatePriorityRequest, Priority>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateServiceRequest, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateStateRequest, State>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdatePriorityRequest, Priority>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateServiceRequest, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateStateRequest, State>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
