using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using arviews_service.API.Models.Trendlog;
using Microsoft.Extensions.Logging;

namespace arviews_service.API.Services
{
    public class TrendlogService : ITrendlogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITrendlogServiceSettings _trendlogServiceSettings;
        private readonly ILogger<TrendlogService> _logger;

        public TrendlogService(IHttpClientFactory httpClientFactory, ITrendlogServiceSettings trendlogServiceSettings, ILogger<TrendlogService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _trendlogServiceSettings = trendlogServiceSettings;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string viewType, string ErrorMessage)> GetViewTypeAsync(int viewId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("TrendlogService");
                var response = await client.GetAsync($"view/{viewId}?code={_trendlogServiceSettings.ApiKey}==tar");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<Trendview>(content, options);
                    return (true, result.Type, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
