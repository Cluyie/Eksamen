using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsApp
{
  /// <summary>
  /// This sents a http get rquest to a api
  /// if you have pre set the base url du you only need to write the rest of the url fx.
  /// if the pre url is https://localhost5001
  /// do you need to set Http to 
  /// /Auth/Login
  /// to hit the post lig in method if you need to send variablse in the url.
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
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public returnObj Get<returnObj>(string Http) where returnObj : class
    {
      returnObj obj;
      using (HttpResponseMessage respons = httpClient.GetAsync(Http).Result)
      {
        obj = ReadAnswer<returnObj>(respons);
      }
      return obj;
    }

    /// <summary>
    /// This maks Put request
    /// retunrObj is the object you expect to get in return
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public HttpResponseMessage Put<returnObj>(string Http, returnObj obj) where returnObj : class
    {
      string myContent = JsonConvert.SerializeObject(obj);
      byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      ByteArrayContent byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return httpClient.PutAsync(Http, byteContent).Result;
    }

    /// <summary>
    /// This makes a Post request
    /// retunrObj is the object you expect to get in return
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public HttpResponseMessage Post<returnObj>(string Http, returnObj obj)
    {
      string myContent = JsonConvert.SerializeObject(obj);
      byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      ByteArrayContent byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return httpClient.PostAsync(Http, byteContent).Result;
    }
    /// <summary>
    /// This sends a delete request
    /// </summary>
    /// <param name="Http"></param>
    /// <returns></returns>
    public HttpResponseMessage Delete(string Http)
    {
      return httpClient.DeleteAsync(Http).Result;
    }

    /// <summary>
    /// Async
    /// This async-method is used to deserialize a HttpResonseMessage that is fx created by an Api wiht Put and Post request
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    public static async Task<returnObj> ReadAnswerAsync<returnObj>(HttpResponseMessage responseMessage) where returnObj : class
    {
      if (responseMessage.IsSuccessStatusCode)
      {
        return await responseMessage.Content.ReadAsAsync<returnObj>().ConfigureAwait(false);
      }
      return null;
    }

    /// <summary>
    /// This non-async method is used to deserialize HttpResonseMessage that is fx created by an Api with Put and Post request
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    public static returnObj ReadAnswer<returnObj>(HttpResponseMessage responseMessage) where returnObj : class
    {
      if (responseMessage.IsSuccessStatusCode)
      {
        return responseMessage.Content.ReadAsAsync<returnObj>().Result;
      }
      return null;
    }

    /// <summary>
    /// This makes a Get request
    /// retunrObj is the object you expect to get in return;
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public async Task<returnObj> GetAsync<returnObj>(string Http) where returnObj : class
    {
      returnObj obj;
      using (HttpResponseMessage response = await httpClient.GetAsync(Http))
      {
        obj = await ReadAnswerAsync<returnObj>(response);
      }
      return obj;
    }

    /// <summary>
    /// This makes a Put request
    /// retunrObj is the object you expect to get in return
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PutAsync<returnObj>(string Http, returnObj obj) where returnObj : class
    {
      string myContent = JsonConvert.SerializeObject(obj);
      byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      ByteArrayContent byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return await httpClient.PutAsync(Http, byteContent);
    }

    /// <summary>
    /// This makes a Post request
    /// returnObj is the object you expect to get in return
    /// </summary>
    /// <typeparam name="returnObj"></typeparam>
    /// <param name="Http"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PostAsync<returnObj>(string Http, returnObj obj) where returnObj : class
    {
      string myContent = JsonConvert.SerializeObject(obj);
      byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      ByteArrayContent byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return await httpClient.PostAsync(Http, byteContent);
    }

    /// <summary>
    /// This sends a delete request
    /// </summary>
    /// <param name="Http"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> DeleteAsync(string Http)
    {
      return await httpClient.DeleteAsync(Http);
    }
  }
}
