using System.Net.Http;
using System.Net.Http.Json;
using ShopCMS.Domain.Interfaces;

namespace ShopCMS.Infrastructure.External
{
    public class ExternalExchangeRateClient : IExchangeRateClient
    {
        private readonly HttpClient _httpClient;

        public ExternalExchangeRateClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(
            string baseCurrency,
            string targetCurrency)
        {
            

            var response = await _httpClient
                .GetFromJsonAsync<ExchangeRateResponse>($"latest/{baseCurrency}");

            if (response == null ||
                !response.Rates.TryGetValue(targetCurrency, out var rate))
                throw new Exception("Exchange rate not available");

            return rate;
        }

        private class ExchangeRateResponse
        {
            public Dictionary<string, decimal> Rates { get; set; } = new();
        }
    }
}
