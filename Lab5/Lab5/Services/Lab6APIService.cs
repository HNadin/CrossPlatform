using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using Lab6.Models;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json.Linq;

namespace Lab5.Services
{
    public class Lab6APIService
    {
        private readonly HttpClient _httpClient;
        private readonly Auth0UserService _auth0UserService;

        public Lab6APIService(HttpClient httpClient, Auth0UserService auth0UserService)
        {
            _httpClient = httpClient;
            _auth0UserService = auth0UserService;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var accessToken = await _auth0UserService.GetApiTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/addresses");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<Address>>(responseStream);
        }

        public async Task<Address> GetAddressAsync(Guid id)
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync($"api/addresses/{id}");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Address>(responseStream);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/customers");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Customer>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Enumerable.Empty<Customer>();
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync($"api/customers/{id}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Customer>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new Customer();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/transactions");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Transaction>>(responseStream);
        }

        public async Task<Transaction> GetTransactionAsync(Guid id)
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync($"api/transactions/{id}");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Transaction>(responseStream);
        }

        public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(DateTime? date, List<string>? transactionTypes, string? valueStart, string? valueEnd)
        {
            await SetAuthorizationHeaderAsync();

            var query = new List<string>();

            if (date.HasValue)
            {
                query.Add($"date={date.Value:yyyy-MM-dd}");
            }

            if (transactionTypes != null && transactionTypes.Any())
            {
                query.Add($"transactionTypes={string.Join(",", transactionTypes)}");
            }

            if (!string.IsNullOrEmpty(valueStart))
            {
                query.Add($"valueStart={valueStart}");
            }

            if (!string.IsNullOrEmpty(valueEnd))
            {
                query.Add($"valueEnd={valueEnd}");
            }

            var queryString = string.Join("&", query);
            var response = await _httpClient.GetAsync($"api/search?{queryString}");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Transaction>>(responseStream);
        }

        public async Task<string> ConvertTimeAsync(DateTime utcDateTime)
        {
            await SetAuthorizationHeaderAsync();

            string dateTimeString = utcDateTime.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            string encodedDateTime = Uri.EscapeDataString(dateTimeString);

            string requestUrl = $"https://192.168.0.105:7225/api/time/convert?utcDateTime={encodedDateTime}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new Exception($"Error calling API: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}
