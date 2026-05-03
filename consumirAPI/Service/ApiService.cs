using consumirAPI.Models;
using consumirAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace consumirAPI.Service
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpClient GetClient()
        {
            var client = _clientFactory.CreateClient("APIClient");

            // Obtém o token da sessão
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            var _httpClient = GetClient();
            var response = await _httpClient.GetAsync($"api/produtos");

            if (response.IsSuccessStatusCode)
            {
                var produtos = await response.Content.ReadFromJsonAsync<IEnumerable<Produto>>();
                return produtos ?? Enumerable.Empty<Produto>();
            }

            return Enumerable.Empty<Produto>();
        }

        public async Task<Produto> GetById(int? id)
        {
            var _httpClient = GetClient();
            var response = await _httpClient.GetAsync($"api/produtos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Produto>();
            }

            return null; 
        }

        public async Task<Produto> Create(Produto produto)
        {
            var _httpClient = GetClient();
            var response = await _httpClient.PostAsJsonAsync("api/produtos", produto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Produto>();
            }

            return null;
        }

        public async Task<bool> Update(int? id, Produto produto)
        {
            var _httpClient = GetClient();
            var response = await _httpClient.PutAsJsonAsync($"api/produtos/{id}", produto);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int? id)
        {
            var _httpClient = GetClient();
            var response = await _httpClient.DeleteAsync($"api/produtos/{id}");

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }
    }
}

