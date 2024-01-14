using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Interfaces;
using SkiServiceModels;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceAPI.Common
{
    public class GenericService<T, TResponseBase, TResponseAdmin, TUpdate, TCreate> : IBaseService<T, TResponseBase, TResponseAdmin, TUpdate, TCreate>
        where T : class, IGenericEFModel
        where TResponseBase : class
        where TResponseAdmin : class, TResponseBase
        where TUpdate : class
        where TCreate : class
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericService(IApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Check if the current user is an Admin
        /// </summary>
        /// <returns>true if admin else false</returns>
        protected bool IsAdmin()
        {
            // to simplfy the frontend for now, should not be like this in production :)

            //var user = _httpContextAccessor.HttpContext?.User;
            //return user?.IsInRole(RoleNames.SuperAdmin.ToString()) ?? false;
            return true;
        }

        /// <summary>
        /// Apply the filter to the query to hide soft-deleted entries from non admin users
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="query">The query to apply the filter on</param>
        /// <returns>the filtered content</returns>
        protected IQueryable<T> ApplyFilter(IQueryable<T> query)
        {
            bool isAdmin = IsAdmin();
            return query.Where(e => isAdmin || e.IsDeleted == false);
        }

        /// <summary>
        /// Helper to distinguish between admin and non admin responses for a single entity
        /// </summary>
        /// <typeparam name="TModel">T</typeparam>
        /// <param name="data">the data to map</param>
        /// <returns>the mapped data in the coresponding DTO</returns>
        protected TaskResult<object> Resolve<TModel>(TModel data)
            where TModel : class, IGenericModel
        {
            if (IsAdmin())
            {
                return TaskResult<object>.Success(_mapper.Map<TResponseAdmin>(data));
            }
            else
            {
                return TaskResult<object>.Success(_mapper.Map<TResponseBase>(data));
            }
        }

        /// <summary>
        /// Helper to distinguish between admin and non admin responses for a list-like entity
        /// </summary>
        /// <typeparam name="TModel">T</typeparam>
        /// <param name="data">the data to map</param>
        /// <returns>the mapped data in the coresponding DTO</returns>
        protected TaskResult<IEnumerable<object>> ResolveList<TModel>(IEnumerable<TModel> data)
            where TModel : class, IGenericModel
        {
            if (IsAdmin())
            {
                return TaskResult<IEnumerable<object>>.Success(_mapper.Map<IEnumerable<TResponseAdmin>>(data));
            }
            else
            {
                return TaskResult<IEnumerable<object>>.Success(_mapper.Map<IEnumerable<TResponseBase>>(data));
            }
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>TResponseBase as TaskResult</returns>
        public virtual async Task<TaskResult<IEnumerable<object>>> GetAllAsync()
        {
            var isAdmin = IsAdmin();
            var query = _context.Set<T>().AsQueryable();
            query = ApplyFilter(query);

            var data = await query.ToListAsync();

            return ResolveList(data);
        }


        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>TResponseBase as TaskResult</returns>
        public virtual async Task<TaskResult<object>> GetAsync(int id)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            return Resolve(resolvedEntity);
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">Entity Data</param>
        /// <returns>TResponseBase as TaskResult</returns>
        public virtual async Task<TaskResult<object>> CreateAsync(TCreate entity)
        {
            var resolvedEntity = _mapper.Map<T>(entity);

            _context.Set<T>().Add(resolvedEntity);
            await _context.SaveChangesAsync();

            return Resolve(resolvedEntity);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <param name="entity">Entity data</param>
        /// <returns>TResponseBase as TaskResult</returns>
        public virtual async Task<TaskResult<object>> UpdateAsync(int id, TUpdate entity)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            _mapper.Map(entity, resolvedEntity);
            await _context.SaveChangesAsync();

            return Resolve(resolvedEntity);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>DeleteResponse as TaskResult</returns>
        public virtual async Task<TaskResult<DeleteResponse>> DeleteAsync(int id)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<DeleteResponse>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            resolvedEntity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return TaskResult<DeleteResponse>.Success(new DeleteResponse
            {
                Count = 1
            });
        }
    }
}
