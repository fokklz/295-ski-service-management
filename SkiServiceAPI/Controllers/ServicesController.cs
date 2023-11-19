    using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for Services
    /// </summary>
    public class ServicesController : GenericController<Service, ServiceResponse, ServiceResponseAdmin, UpdateServiceRequest, CreateServiceRequest>
    {
        public ServicesController(GenericService<Service, ServiceResponse, ServiceResponseAdmin, UpdateServiceRequest, CreateServiceRequest> service) : base(service)
        {
        }
    }
}
