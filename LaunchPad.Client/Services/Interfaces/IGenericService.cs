using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LaunchPad.Client.Services.Interfaces
{
    public interface IGenericService<TEntity, TSummary, TLookUp, in TFilter> where TEntity : class
    {
        void SetHttpClient(HttpClient httpClient);

        /// <summary>
        /// Get First
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetFirstAsync();

        /// <summary>
        /// Get Last
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetLastAsync();

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TSummary>> GetAllSummaryAsync();

        /// <summary>
        /// Get LookUp 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TLookUp>> GetLookUpAsync(bool isActive);

        /// <summary>
        /// Get Filtered
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> GetFilteredAsync(TFilter filter);

        /// <summary>
        /// Get Filtered Summary
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter);

        /// <summary>
        /// Get Entity instance using Id to either create new or get existing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetInstanceAsync(int id);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Get Count
        /// </summary>
        /// <returns>Entity count</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Get Summary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TSummary> GetSummaryAsync(int id);

        /// <summary>
        /// Generic method which determines whether to Add / Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);

        ///// <summary>
        ///// Delete Range
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task DeleteRangeAsync(int[] id);
    }
}
