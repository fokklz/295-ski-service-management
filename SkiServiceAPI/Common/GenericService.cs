using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
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

        public GenericService(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<List<TResponse>>> GetAllAsync()
        {
            List<TResponse> destinations = new List<TResponse>();
            var result = await _context.Set<T>().ToListAsync();
            foreach (var res in result)
            {
                destinations.Add(_mapper.Map<TResponse>(res));
            }
            return TaskResult<List<TResponse>>.Success(destinations);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> GetAsync(int id)
        {
            var user = await _context.Set<T>().FindAsync(id);
            if (user == null) return TaskResult<TResponse>.Error("Entry not Found");
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(user));
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">Entity Data</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> CreateAsync(TCreate entity)
        {
            var parsed = _mapper.Map<T>(entity);

            bool valid = await parsed.ValidateAsync();
            if (!valid) TaskResult<TResponse>.Error("Validation failed");

            _context.Set<T>().Add(parsed);
            await _context.SaveChangesAsync();
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(parsed));
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <param name="entity">Entity data</param>
        /// <returns>TResponse as TaskResult</returns>
        public virtual async Task<TaskResult<TResponse>> UpdateAsync(int id, TUpdate entity)
        {
            var current = await _context.Set<T>().FindAsync(id);
            if (current == null) return TaskResult<TResponse>.Error("Entry not Found");

            _mapper.Map(entity, current);
            await _context.SaveChangesAsync();
            return TaskResult<TResponse>.Success(_mapper.Map<TResponse>(current));
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>DeleteResponse as TaskResult</returns>
        public virtual async Task<TaskResult<DeleteResponse>> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return TaskResult<DeleteResponse>.Error("Entry not Found");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return TaskResult<DeleteResponse>.Success(new DeleteResponse
            {
                Id = entity.Id,
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

        public GenericService(IApplicationDBContext context, IMapper mapper) :
            base(context, mapper)
        {
        }
    }
}
