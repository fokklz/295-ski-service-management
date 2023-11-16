using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for States
    /// </summary>
    public class StatesController : GenericController<State, UpdateStateRequest, CreateStateRequest>
    {
        public StatesController(GenericService<State, UpdateStateRequest, CreateStateRequest> service) : base(service)
        {
        }
    }
}
