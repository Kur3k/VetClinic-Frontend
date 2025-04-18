﻿using Newtonsoft.Json;
using Projekt.Factory;
using RestSharp;

namespace Projekt.Model;

public class OwnerManager
{
    public OwnerManager()
    {
        _restClient = new CustomRestClientClientFactory().CreateRestClient(_baseUrl);
    }

    public async Task<List<Owner>?> GetOwners()
    {
        string requestUrl = "Owner/GetOwners";
        var response = await _restClient.GetAsync(new RestRequest(requestUrl));

        if (response != null && response.IsSuccessful && response.Content != null)
        {
            return JsonConvert.DeserializeObject<List<Owner>>(response.Content) ?? throw new Exception("Coś poszło nie tak");
        }

        return null;
    }

    public async Task AddOwner(Owner owner)
    {
        string requestUrl = "Owner/AddOwner";
        var request = new RestRequest(requestUrl, Method.Post);
        request.AddJsonBody(owner);

        var response = await _restClient.ExecuteAsync(request);
    }

    public async Task UpdateOwner(Owner owner)
    {
        string requestUrl = $"Owner/UpdateOwner/{owner.Id}";

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
        string requestUrl = $"Owner/DeleteOwner/{owner.Id}"; 

        var response = await _restClient.DeleteAsync(new RestRequest(requestUrl));
        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<bool>(response.Content);
        }
    }

    private RestClient _restClient;

    private const string _baseUrl = "https://localhost:7000/";
}
