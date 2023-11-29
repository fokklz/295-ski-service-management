using SkiServiceAPI.Common;
using SkiServiceModels;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;

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
