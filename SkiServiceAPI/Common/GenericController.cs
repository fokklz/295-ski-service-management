using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;

namespace SkiServiceAPI.Common
{
    /// <summary>
    /// Generic Controller for CRUD operations
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// <typeparam name="TResponse">Response DTO</typeparam>
    /// <typeparam name="TUpdate">Update DTO</typeparam>
    /// <typeparam name="TCreate">Create DTO</typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<T, TResponse, TUpdate, TCreate> : ControllerBase
        where T : class, IGenericModel
        where TResponse : class
        where TUpdate : class
        where TCreate : class
    {

        private readonly IBaseService<T, TResponse, TUpdate, TCreate> _service;

        public GenericController(IBaseService<T, TResponse, TUpdate, TCreate> service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>TDestination mapped from T</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>TDestination mapped from T</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            return result.IsOk ? Ok(result.Response) : NotFound(result.Message);
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">Entity Data</param>
        /// <returns>TDestination mapped from T</returns>
        [HttpPost]
        [Authorize(Roles = nameof(RoleNames.SuperAdmin))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] TCreate entity)
        {
            var result = await _service.CreateAsync(entity);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity Data</param>
        /// <returns>TDestination mapped from T</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleNames.SuperAdmin))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, [FromBody] TUpdate entity)
        {
            var result = await _service.UpdateAsync(id, entity);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>TDestination mapped from T</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleNames.SuperAdmin))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }
    }

    /// <summary>
    /// Allows to use the same type for T, TResponse since they are often the same specially on small models
    /// </summary>
    /// <typeparam name="T">Target Model</typeparam>
    /// <typeparam name="TUpdate">Update DTO of Model</typeparam>
    /// <typeparam name="TCreate">Create DTO of Model</typeparam>
    public class GenericController<T, TUpdate, TCreate> : GenericController<T, T, TUpdate, TCreate>
        where T : class, IGenericModel
        where TUpdate : class
        where TCreate : class
    {
        public GenericController(IBaseService<T, T, TUpdate, TCreate> service)
        : base(service)
        {
        }
    }
}
