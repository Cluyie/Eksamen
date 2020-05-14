using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthCredentialsKeeper _credentialsKeeper;

        //private const string BASE_URL = "http://81.27.216.103/MobileBff/";
        private const string BASE_URL = "http://localhost:5001/";
        private const string TOKEN_HEADER_NAME = "Authorization";

        private bool _tokenHeaderIsSet = false;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiClient(HttpClient httpClient, AuthCredentialsKeeper credentialsKeeper)
        {
            _httpClient = httpClient;
            _credentialsKeeper = credentialsKeeper;
        }

        public async Task<ApiResponseDTO<T>> GetAsync<T>(string url)
        {
            SetTokenHeader();

            var response = await _httpClient.GetAsync(WrapUrl(url));

            using (var responseContent = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ApiResponseDTO<T>>(responseContent, _jsonOptions);
            }
        }

        public async Task<ApiResponseDTO<T>> DeleteAsync<T>(string url)
        {
            SetTokenHeader();

            var response = await _httpClient.DeleteAsync(WrapUrl(url));

            using (var responseContent = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ApiResponseDTO<T>>(responseContent, _jsonOptions);
            }
        }

        public async Task<ApiResponseDTO<T>> PostAsync<T>(string url, object data)
        {
            SetTokenHeader();
            string s = JsonSerializer.Serialize(data);
            Console.WriteLine(s);
            var httpContent = new StringContent(s, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(WrapUrl(url), httpContent);

            using (var responseContent = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ApiResponseDTO<T>>(responseContent, _jsonOptions);
            }
        }

        public async Task<ApiResponseDTO<T>> PutAsync<T>(string url, object data)
        {
            SetTokenHeader();

            var httpContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(WrapUrl(url), httpContent);

            using (var responseContent = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ApiResponseDTO<T>>(responseContent, _jsonOptions);
            }
        }

        private string WrapUrl(string url)
        {
            return BASE_URL + url.TrimStart('/');
        }

        private void SetTokenHeader()
        {
            if (_tokenHeaderIsSet && !_credentialsKeeper.HasCredentials())
            {
                _httpClient.DefaultRequestHeaders.Remove(TOKEN_HEADER_NAME);
                _tokenHeaderIsSet = false;
            }
            else if (!_tokenHeaderIsSet && _credentialsKeeper.HasCredentials())
            {
                _httpClient.DefaultRequestHeaders.Add(TOKEN_HEADER_NAME, _credentialsKeeper.Token);
                _tokenHeaderIsSet = true;
            }
        }
    }
}