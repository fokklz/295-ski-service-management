using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using System.Collections.Generic;

namespace SkiServiceAPI.Common
{
    public class GenericService<T, TResponse, TUpdate, TCreate> : IBaseService<T, TResponse, TUpdate, TCreate>
        where T : class, IGenericModel
        where TResponse : class
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
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole(RoleNames.SuperAdmin.ToString()) ?? false;
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
        /// Get all entities
        /// </summary>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<List<TResponse>>> GetAllAsync()
        {
            var query = _context.Set<T>();
            var filtered = ApplyFilter(query);
            var resolvedEntity = await filtered.ToListAsync();
            return TaskResult<List<TResponse>>.Success(_mapper.Map<List<TResponse>>(resolvedEntity));
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> GetAsync(int id)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<TResponse>.Error("Entry not Found");
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(resolvedEntity));
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">Entity Data</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> CreateAsync(TCreate entity)
        {
            var resolvedEntity = _mapper.Map<T>(entity);

            _context.Set<T>().Add(resolvedEntity);
            await _context.SaveChangesAsync();
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(resolvedEntity));
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <param name="entity">Entity data</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> UpdateAsync(int id, TUpdate entity)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<TResponse>.Error("Entry not Found");

            _mapper.Map(entity, resolvedEntity);
            await _context.SaveChangesAsync();
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(resolvedEntity));
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>DeleteResponse as TaskResult</returns>
        public virtual async Task<TaskResult<DeleteResponse>> DeleteAsync(int id)
        {
            var resolvedEntity = await _context.Set<T>().FindAsync(id);
            if (resolvedEntity == null || (resolvedEntity.IsDeleted && !IsAdmin())) return TaskResult<DeleteResponse>.Error("Entry not Found");

            resolvedEntity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return TaskResult<DeleteResponse>.Success(new DeleteResponse
            {
                Id = resolvedEntity.Id,
            });
        }
    }

    /// <summary>
    /// Allows to use the same type for T, TResponse since they are often the same specially on small models
    /// </summary>
    /// <typeparam name="T">Target Model</typeparam>
    /// <typeparam name="TUpdate">Update DTO of Model</typeparam>
    /// <typeparam name="TCreate">Create DTO of Model</typeparam>
    public class GenericService<T, TUpdate, TCreate> : GenericService<T, T, TUpdate, TCreate>
        where T : class, IGenericModel
        where TUpdate : class
        where TCreate : class
    {

        public GenericService(IApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) :
            base(context, mapper, httpContextAccessor)
        {
        }
    }
}
