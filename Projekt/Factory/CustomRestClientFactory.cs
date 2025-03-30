using RestSharp;
using RestSharp.Serializers.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projekt.Factory
{
    public class CustomRestClientClientFactory
    {
        public RestClient CreateRestClient(string baseUrl)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            RestClient restClient = new RestClient(
                httpClient: httpClient,
                disposeHttpClient: true,
                configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                }),
                configureRestClient: c => c.ThrowOnDeserializationError = true);
            return restClient;
        }
    }
}
