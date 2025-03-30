using Newtonsoft.Json;
using Projekt.Factory;
using RestSharp;

namespace Projekt.Model;

public class PetMenager
{
    public PetMenager()
    {
        _restClient = new CustomRestClientClientFactory().CreateRestClient(_baseUrl);
    }

    public async Task<List<Pet>?> GetPet()
    {
        string requestUrl = $"Pet/GetPets";

        var response = await _restClient.GetAsync(new RestRequest(requestUrl));

        if (response != null && response.IsSuccessful && response.Content != null)
        {
            return JsonConvert.DeserializeObject<List<Pet>>(response.Content) ?? throw new Exception("Something goes wrong");
        }

        return null;
    }

    public async Task AddPet(Pet pet)
    {
        string requestUrl = $"Pet/AddPet";

        var request = new RestRequest(requestUrl, Method.Post);
        request.AddJsonBody(pet);

        var response = await _restClient.ExecuteAsync(request);
    }

    public async Task UpdatePet(Pet pet)
    {
        string requestUrl = $"Pet/UpdatePet/{pet.Id}"; 
       
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
        string requestUrl = $"Pet/DeletePet/{pet.Id}";

        var response = await _restClient.DeleteAsync(new RestRequest(requestUrl));
        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<bool>(response.Content);
            
        }
    }

    private RestClient _restClient;

    private const string _baseUrl = "https://localhost:7000/";
}
