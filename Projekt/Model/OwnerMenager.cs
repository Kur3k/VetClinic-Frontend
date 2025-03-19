using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projekt.Model;

public class OwnerManager
{
    private RestClient _restClient;

    public OwnerManager()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7000/");
        
        _restClient = new RestClient(
            httpClient: httpClient,
            disposeHttpClient: true,
            configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            }),
            configureRestClient: c => c.ThrowOnDeserializationError = true);
    }

    public async Task<List<Owner>?> GetOwners()
    {
        string requestUrl = "https://localhost:7000/Owner/GetOwners"; 

        var response = await _restClient.GetAsync(new RestRequest(requestUrl));

        if (response != null && response.IsSuccessful && response.Content != null)
        {
            return JsonConvert.DeserializeObject<List<Owner>>(response.Content) ?? throw new Exception("Coś poszło nie tak");
        }

        return null;
    }

    public async Task AddOwner(Owner owner)
    {
        string requestUrl = "https://localhost:7000/Owner/AddOwner";
        var request = new RestRequest(requestUrl, Method.Post);
        request.AddJsonBody(owner);

        var response = await _restClient.ExecuteAsync(request);
    }

    public async Task UpdateOwner(Owner owner)
    {
        string requestUrl = $"https://localhost:7000/Owner/UpdateOwner/{owner.Id}";

        var request = new RestRequest(requestUrl, Method.Put);
        request.AddJsonBody(owner);

        var response = await _restClient.ExecuteAsync(request);

        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<bool>(response.Content);
            if (result == false)
            {
                throw new Exception("Aktualizacja nie powiodła się");
            }
        }
        else
        {
            throw new Exception("Błąd podczas próby aktualizacji");
        }
    }

    public async Task DeleteOwner(Owner owner)
    {
        string requestUrl = $"https://localhost:7000/Owner/DeleteOwner/{owner.Id}"; 

        var response = await _restClient.DeleteAsync(new RestRequest(requestUrl));
        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<bool>(response.Content);
        }
    }
}
