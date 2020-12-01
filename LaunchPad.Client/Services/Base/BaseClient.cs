using LaunchPad.Client.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LaunchPad.Client.Services.Base
{
    public class BaseClient
    {
        protected HttpClient _client;
        private string domain;

        public BaseClient(HttpClient client)
        {
            _client = client;
        }

        public BaseClient(HttpClient client, string domain) : this(client)
        {
            this.domain = domain;
        }

        /// <summary>
        /// Get Single
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string url) where T : new()
        {
            T result;

            try
            {
                var response = await _client.GetStringAsync(url);

                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response, new IsoDateTimeConverter { Culture = new CultureInfo("en-GB") }));
            }
            catch (Exception exception)
            {
                result = default;
            }

            return result;
        }

        /// <summary>
        /// Get Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<ICollection<T>> GetIEnumerableAsync<T>(string url)
        {
            ICollection<T> result;
            try
            {
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                result = await Task.Run(() => JsonConvert.DeserializeObject<ICollection<T>>(responseContent, new IsoDateTimeConverter { Culture = new CultureInfo("EN-GB") }));
            }
            catch (Exception exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Get Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<ICollection<T>> GetArrayAsync<T>(string url)
        {
            ICollection<T> result;
            try
            {
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                result = await Task.Run(() => JsonConvert.DeserializeObject<T[]>(responseContent, new IsoDateTimeConverter { Culture = new CultureInfo("EN-GB") }));
            }
            catch (Exception exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string url, T entity)
        {
            try
            {
                var content = JsonConvert.SerializeObject(entity);

                var response = await _client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseContent));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string url, T entity)
        {
            var content = JsonConvert.SerializeObject(entity);

            try
            {
                var response = await _client.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

                var responseContent = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseContent));
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string url)
        {
            try
            {
                var response = await _client.DeleteAsync(url);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(string url, T entity)
        {
            var response = await _client.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// PUT Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> PutEnumerableAsync<T>(string url, IEnumerable<T> entity)
        {
            var content = JsonConvert.SerializeObject(entity);

            var response = await _client.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var x = await response.Content.ReadAsStringAsync();

                var xd = JsonConvert.DeserializeObject(x);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            try
            {
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(responseContent));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> PostEnumerableAsync<T>(string url, IEnumerable<T> entity)
        {
            var content = JsonConvert.SerializeObject(entity);

            var response = await _client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                var error = JsonConvert.DeserializeObject(errorContent);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            try
            {
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(responseContent));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
