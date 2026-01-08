using ShopCMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using ShopCMS.Infrastructure.Exceptions;


namespace ShopCMS.Infrastructure.External
{

    

    public class CurrencyRateProvider : ICurrencyRateProvider
    {
        private readonly HttpClient _httpClient;

        public CurrencyRateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetRateAsync(string from, string to)
        {
            var response = await _httpClient
                .GetFromJsonAsync<RateResponse>($"rates?from={from}&to={to}");

            if (response == null)
                throw new ExternalServiceException("Invalid currency response");

            return response.Rate;
        }
    }

    public record RateResponse(decimal Rate);
}