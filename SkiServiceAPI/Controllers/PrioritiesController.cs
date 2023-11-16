using SkiServiceAPI.Common;
using SkiServiceAPI.Models;
using SkiServiceAPI.DTOs;
using SkiServiceAPI.DTOs.Requests;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for Priorities
    /// </summary>
    public class PrioritiesController : GenericController<Priority, UpdatePriorityRequest, CreatePriorityRequest>
    {
        public PrioritiesController(GenericService<Priority, UpdatePriorityRequest, CreatePriorityRequest> service) : base(service)
        {
        }
    }
}
