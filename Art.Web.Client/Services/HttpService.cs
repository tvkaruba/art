using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Art.Web.Client.Services.Abstractions;
using Art.Web.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        private readonly NavigationManager _navigationManager;

        private readonly ILocalStorageService _localStorageService;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            _navigationManager = navigationManager ?? throw new ArgumentException(nameof(navigationManager));
            _localStorageService = localStorageService ?? throw new ArgumentException(nameof(localStorageService));
        }

        public async Task<T> Get<T>(string uri, IDictionary<string, object> filters = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var user = await _localStorageService.GetItem<PersonLoginGet>("user");
            if (user != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
            }

            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout", forceLoad: true);
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task Put(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            var user = await _localStorageService.GetItem<PersonLoginGet>("user");
            if (user != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
            }

            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            var user = await _localStorageService.GetItem<PersonLoginGet>("user");
            if (user != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
            }

            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task Delete(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            var user = await _localStorageService.GetItem<PersonLoginGet>("user");
            if (user != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
            }

            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
    }
}
