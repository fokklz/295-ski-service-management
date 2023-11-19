using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for States
    /// </summary>
    public class StatesController : GenericController<State, StateResponse, StateResponseAdmin, UpdateStateRequest, CreateStateRequest>
    {
        public StatesController(GenericService<State, StateResponse, StateResponseAdmin, UpdateStateRequest, CreateStateRequest> service) : base(service)
        {
        }
    }
}
