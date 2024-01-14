using SkiServiceAPI.Common;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Models.EF;

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
