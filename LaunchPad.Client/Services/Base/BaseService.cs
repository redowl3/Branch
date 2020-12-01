using LaunchPad.Client.Helpers;
using LaunchPad.Model.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace LaunchPad.Client.Services.Base
{
    public abstract class BaseService<TEntity, TSummary, TLookUp, TFilter> : BaseClient
        where TEntity : BaseEntity, new()
        where TSummary : new()
    {
        protected string BaseResource;

        private const string resourceFirst = "first";
        private const string resourceLast = "last";
        private const string resourceAll = "all";
        private const string resourceCount = "count";
        private const string resourceSummaryFiltered = "summary/filtered";
        private const string resourceFiltered = "filtered";
        private const string resourceSummaryAll = "summary/all";
        private const string resourceSummary = "summary";
        private const string resourceLookUp = "lookup";

        protected BaseService(HttpClient httpClient) : base(httpClient)
        { }

        protected BaseService(HttpClient httpClient, string domain) : base(httpClient)
        {
            if (!string.IsNullOrEmpty(domain))
            {
                var subDomainUrl = new Uri($"https://{domain}.roco-api.co.uk/admin/api/v1/");

                httpClient.BaseAddress = subDomainUrl;
            }
        }

        /// <summary>
        /// Used in testing
        /// </summary>
        /// <param name="httpClient"></param>
        public void SetHttpClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// Get Instance regardless of new or existing TEntity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetInstanceAsync(int id)
        {
            if (id == 0)
            {
                return new TEntity() { status = "active", created_at = DateTime.Now, datestamp = DateTime.Now };
            }
            else
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

                return await GetAsync<TEntity>(url);
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            return await GetAsync<TEntity>(url);
        }

        public async Task<int> GetCountAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceCount);

            return await GetAsync<int>(url);
        }

        public async Task<TEntity> GetAsyncString(string id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            return await GetAsync<TEntity>(url);
        }

        public async Task<TEntity> GetFirstAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceFirst);

            return await GetAsync<TEntity>(url);
        }

        public async Task<TEntity> GetLastAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceLast);

            return await GetAsync<TEntity>(url);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceAll);

            return await GetIEnumerableAsync<TEntity>(url);
        }

        public async Task<IEnumerable<TSummary>> GetAllSummaryAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceSummaryAll);

            return await GetIEnumerableAsync<TSummary>(url);
        }


        public async Task<ICollection<TEntity>> GetFilteredAsync(TFilter filter)
        {
            var queryString = filter.ToQueryString();

            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}?{2}", BaseResource, resourceFiltered, queryString);

            return await GetIEnumerableAsync<TEntity>(url);
        }

        public async Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter)
        {
            var queryString = filter.ToQueryString();

            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}?{2}", BaseResource, resourceSummaryFiltered, queryString);

            return await GetIEnumerableAsync<TSummary>(url);
        }

        public async Task<TSummary> GetSummaryAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceSummary, id);

            return await GetAsync<TSummary>(url);
        }

        public async Task<IEnumerable<TLookUp>> GetLookUpAsync(bool isActive)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceLookUp, isActive);

            return await GetIEnumerableAsync<TLookUp>(url);
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            return entity.id > 0 ? await UpdateAsync(entity) : await AddAsync(entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}", BaseResource);

            return await PostAsync(url, entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}", BaseResource);

            return await PutAsync(url, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            await DeleteAsync(url);
        }

    }
}
