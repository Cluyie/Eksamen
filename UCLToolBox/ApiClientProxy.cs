using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models;
using Models.Interfaces;

namespace UCLToolBox
{
    /// <summary>
    /// This sends a http get request to a api
    /// if you have pre set the base url du you only need to write the rest of the url fx.
    /// if the pre url is http://localhost:5000
    /// do you need to set Http to 
    /// /Auth/Login
    /// to hit the post in method if you need to send variables in the url.
    /// </summary>
    public class ApiClientProxy
    {
        public HttpClient httpClient;
        public ApiClientProxy(HttpClient http)
        {
            httpClient = http;
        }

        /// <summary>
        /// This makes a Get request
        /// retunrObj is the object you expect to get in return
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public TReturnObj Get<TReturnObj>(string http) where TReturnObj : class
        {
            TReturnObj obj;
            using (HttpResponseMessage response = httpClient.GetAsync(http).Result)
            {
                obj = ReadAnswer<TReturnObj>(response);
            }
            return obj;
        }

        /// <summary>
        /// Generates an error message from an api response, and optionally a http response. d
        /// If all else fails, returns a ApiResponseCode.BadRequest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public string GenerateErrorMessage<T>(IApiResponse<T> result, HttpResponseMessage response = null) where T : class
        {
            ApiResponseCode? statusResponseCode = null;
            if (Enum.TryParse<ApiResponseCode>(response?.StatusCode.ToString(), out var responseCode))
                statusResponseCode = responseCode;

            //Enum.GetName(typeof(HttpStatusCode),    
            return (result?.Code ?? statusResponseCode ?? ApiResponseCode.BadRequest).GetAttribute<DisplayAttribute>().Name;
        }

        /// <summary>
        /// This makes a Put request
        /// returnObj is the object you expect to get in return
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public HttpResponseMessage Put<TReturnObj>(string http, TReturnObj obj) where TReturnObj : class
        {
            string myContent = JsonConvert.SerializeObject(obj);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PutAsync(http, byteContent).Result;
        }

        /// <summary>
        /// This makes a Post request
        /// retunrObj is the object you expect to get in return
        /// </summary>
        /// <typeparam name="returnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public HttpResponseMessage Post<returnObj>(string http, returnObj obj)
        {
            string myContent = JsonConvert.SerializeObject(obj);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(http, byteContent).Result;
        }
        /// <summary>
        /// This sends a delete request
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(string http)
        {
            return httpClient.DeleteAsync(http).Result;
        }

        /// <summary>
        /// Async
        /// This async-method is used to deserialize a HttpResonseMessage that is fx created by an Api wiht Put and Post request
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static async Task<TReturnObj> ReadAnswerAsync<TReturnObj>(HttpResponseMessage responseMessage) where TReturnObj : class
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<TReturnObj>().ConfigureAwait(false);
            }
            return null;
        }

        /// <summary>
        /// This non-async method is used to deserialize HttpResonseMessage that is fx created by an Api with Put and Post request
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static TReturnObj ReadAnswer<TReturnObj>(HttpResponseMessage responseMessage) where TReturnObj : class
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return responseMessage.Content.ReadAsAsync<TReturnObj>().Result;
            }
            return null;
        }

        /// <summary>
        /// This makes a Get request
        /// retunrObj is the object you expect to get in return;
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public async Task<TReturnObj> GetAsync<TReturnObj>(string http) where TReturnObj : class
        {
            TReturnObj obj;
            using (HttpResponseMessage response = await httpClient.GetAsync(http))
            {
                obj = await ReadAnswerAsync<TReturnObj>(response);
            }
            return obj;
        }

        /// <summary>
        /// This makes a Put request
        /// retunrObj is the object you expect to get in return
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync<TReturnObj>(string http, TReturnObj obj) where TReturnObj : class
        {
            string myContent = JsonConvert.SerializeObject(obj);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await httpClient.PutAsync(http, byteContent);
        }

        /// <summary>
        /// This makes a Post request
        /// returnObj is the object you expect to get in return
        /// </summary>
        /// <typeparam name="TReturnObj"></typeparam>
        /// <param name="http"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<TReturnObj>(string http, TReturnObj obj) where TReturnObj : class
        {
            string myContent = JsonConvert.SerializeObject(obj);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await httpClient.PostAsync(http, byteContent);
        }

        /// <summary>
        /// This sends a delete request
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string http)
        {
            return await httpClient.DeleteAsync(http);
        }
    }
}