using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for Services
    /// </summary>
    public class ServicesController : GenericController<Service, UpdateServiceRequest, CreateServiceRequest>
    {
        public ServicesController(GenericService<Service, UpdateServiceRequest, CreateServiceRequest> service) : base(service)
        {
        }
    }
}
