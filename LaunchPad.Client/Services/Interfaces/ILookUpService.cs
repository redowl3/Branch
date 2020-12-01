using LaunchPad.Model.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace LaunchPad.Client.Services.Interfaces
{
    public interface ILookUpService<TEntity> where TEntity : BaseLookUp
    {
        void SetHttpClient(HttpClient httpClient);

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get Filtered
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync(int parentId);
    }
}
