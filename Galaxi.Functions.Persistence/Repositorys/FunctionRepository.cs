using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace Galaxi.Functions.Persistence.Repositorys
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly FunctionContextDb _context;
        private readonly IDistributedCache _cache;
        private readonly ILogger<FunctionRepository> _log;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);
        private const string _cacheKeyAllFunctions = "all_functions";
        private const string _cacheKeyFunction = "function_";
        private const string _cacheKeyFunctionByMovieId = "functionByMovieId_";

        public FunctionRepository(FunctionContextDb context, IDistributedCache cache, ILogger<FunctionRepository> log)
        {
            _context = context;
            _cache = cache;
            _log = log;
        }

        public async Task Add(Function function) 
        {
            _context.Add(function);
            await RemoveCacheAsync(movieId: function.MovieId);
        }
        public async Task Delete(Function function) 
        {
            _context.Remove(function);
            await RemoveCacheAsync(function.FunctionId, function.MovieId);
        }

        public async Task Update(Function function)
        {
            _context.Update(function);
            await RemoveCacheAsync(function.FunctionId, function.MovieId);
        }

        public async Task<Function> GetFunctionByIdAsync(Guid functionId)
        {
            var cacheKey = $"{_cacheKeyFunction}{functionId}";

            var cacheFunction = await GetCacheAsync<Function>(cacheKey);
            if (cacheFunction != null)
            {
                return cacheFunction;
            }

            var function = await _context.MovieFunction.FirstOrDefaultAsync(u => u.FunctionId == functionId);
            if (function != null)
            {
                _ = SetCacheAsync(function, cacheKey);
            }
            return function;
        }

        public async Task<IEnumerable<Function>> GetFunctionByMovieIdAsync(Guid movieId)
        {
            var cacheKey = $"{_cacheKeyFunctionByMovieId}{movieId}";

            var cacheFunctionBymovieId = await GetCacheAsync<IEnumerable<Function>>(cacheKey);
            if (cacheFunctionBymovieId != null)
            {
                return cacheFunctionBymovieId;
            }

            var functionByMovieId = await _context.MovieFunction.ToListAsync();
            if (movieId != null)
            {
                functionByMovieId = functionByMovieId.Where(x => x.MovieId.Equals(movieId)).ToList();
            }
            if (functionByMovieId != null && functionByMovieId.Any())
            {
                _ = SetCacheAsync(functionByMovieId, cacheKey);
            }

            return functionByMovieId;
        }

        public async Task<IEnumerable<Function>> GetFunctionsAsync()
        {
            var cacheFunctions = await GetCacheAsync<IEnumerable<Function>>(_cacheKeyAllFunctions);

            if (cacheFunctions != null)
            {
                return cacheFunctions;
            }
            var functions = await _context.MovieFunction.ToListAsync();
            if (functions != null && functions.Any())
            {
                _ = SetCacheAsync(functions, _cacheKeyAllFunctions);
            }
            return functions;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task SetCacheAsync<T>(T entity, string cacheKey)
        {
            try
            {
                await _cache.SetStringAsync
                             (cacheKey, JsonConvert.SerializeObject(entity),
                               new DistributedCacheEntryOptions
                               {
                                   AbsoluteExpirationRelativeToNow = _cacheExpiration
                               }
                             );
            }
            catch (Exception ex)
            {
                _log.LogWarning("Failed to set cache in Redis", ex);
            }

        }

        private async Task<T> GetCacheAsync<T>(string cacheKey) where T : class
        {
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    return JsonConvert.DeserializeObject<T>(cachedData);
                }
            }
            catch (RedisException ex)
            {
                _log.LogWarning($"Failed to retrieve cache in Redis for key {cacheKey}", ex);
            }
            catch (Exception ex)
            {
                _log.LogWarning($"An unexpected error occurred when retrieving cache for key {cacheKey}", ex);
            }
            return null;
        }

        private async Task RemoveCacheAsync(Guid? filmId = null, Guid? movieId = null)
        {
            try
            {
                await Task.WhenAll(
                        _cache.RemoveAsync($"{_cacheKeyFunction}{filmId}"),
                        _cache.RemoveAsync($"{_cacheKeyFunctionByMovieId}{movieId}"),
                        _cache.RemoveAsync(_cacheKeyAllFunctions)
                    );
            }
            catch (RedisException ex)
            {
                _log.LogWarning($"Failed to remove cache in Redis for movie {filmId}", ex);
            }
            catch (Exception ex)
            {
                _log.LogWarning($"An unexpected error occurred when removing cache for movie {filmId}", ex);
            }
        }
    }
}
