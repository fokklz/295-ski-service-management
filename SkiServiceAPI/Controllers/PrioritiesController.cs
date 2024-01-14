using SkiServiceAPI.Common;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for Priorities
    /// </summary>
    public class PrioritiesController : GenericController<Priority, PriorityResponse, PriorityResponseAdmin, UpdatePriorityRequest, CreatePriorityRequest>
    {
        public PrioritiesController(GenericService<Priority, PriorityResponse, PriorityResponseAdmin, UpdatePriorityRequest, CreatePriorityRequest> service) : base(service)
        {
        }
    }
}
