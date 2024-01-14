using AutoMapper;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Models.Base;
using SkiServiceModels.Models.EF;

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
            CreateMap<User, UserResponseAdmin>();
            CreateMap<Service, ServiceResponse>();
            CreateMap<Service, ServiceResponseAdmin>();
            CreateMap<State, StateResponse>();
            CreateMap<State, StateResponseAdmin>();
            CreateMap<Priority, PriorityResponse>();
            CreateMap<Priority, PriorityResponseAdmin>();
            CreateMap<Order, OrderResponse>();
            CreateMap<Order, OrderResponseAdmin>();

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

            CreateMap<CreateOrderRequest, Order>()
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

            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
