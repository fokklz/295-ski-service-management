using SkiServiceAPI.Common;
using SkiServiceAPI.Models;
using SkiServiceAPI.DTOs;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;

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
