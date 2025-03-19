using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projekt.Model;

public class PetMenager
{
    private RestClient _restClient;

    private string BaseURl = "https://localhost:7000/";
    private string BasePetURl = "https://localhost:7000/Pet/";

    public PetMenager()
    {
        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(BaseURl);
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

    public async Task<List<Pet>?> GetPet()
    {
        string requestUrl = $"{BasePetURl}GetPets";


        var response = await _restClient.GetAsync(new RestRequest(requestUrl));

        if (response != null && response.IsSuccessful && response.Content != null)
        {
            return JsonConvert.DeserializeObject<List<Pet>>(response.Content) ?? throw new Exception("Something goes wrong");
        }

        return null;
    }

    public async Task AddPet(Pet pet)
    {
        string requestUrl = $"{BasePetURl}AddPet";

        var request = new RestRequest(requestUrl, Method.Post);
        request.AddJsonBody(pet);

        var response = await _restClient.ExecuteAsync(request);
    }

    public async Task UpdatePet(Pet pet)
    {
        string requestUrl = $"{BasePetURl}UpdatePet/{pet.Id}"; 
       
        var request = new RestRequest(requestUrl, Method.Put);
        request.AddJsonBody(pet);

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


    public async Task DeletePet(Pet pet)
    {
        string requestUrl = $"{BaseURl}DeletePet/{pet.Id}";

        var response = await _restClient.DeleteAsync(new RestRequest(requestUrl));
        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<bool>(response.Content);
            
        }
    }
}
