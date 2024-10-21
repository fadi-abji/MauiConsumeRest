using System.Net.Http.Json;
using System.Text.Json;

namespace PartsClient.Data;

public static class PartsManager
{
    // TODO: Add fields for BaseAddress, Url, and authorizationKey
    static readonly string BaseAddress = "https://mslearnpartsserver2005722695.azurewebsites.net";
    static readonly string Url = $"{BaseAddress}/api/";
    private static string authorizationKey;

    static HttpClient client;

    private static async Task<HttpClient> GetClient()
    {
        if (client != null)
            return client;

        client = new HttpClient();

        if (string.IsNullOrEmpty(authorizationKey))
        {
            authorizationKey = await client.GetStringAsync($"{Url}login");
            authorizationKey = JsonSerializer.Deserialize<string>(authorizationKey);
        }

        client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        return client;
    }

    public static async Task<IEnumerable<Part>> GetAll()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new List<Part>();

        var client = await GetClient();
        string result = await client.GetStringAsync($"{Url}parts");

        return JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public static async Task<Part> Add(string partName, string supplier, string partType)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new Part();
        var part = new Part()
        {
            PartName = partName,
            Suppliers = new List<string>(new[] { supplier }),
            PartID = string.Empty,
            PartType = partType,
            PartAvailableDate = DateTime.Now.Date
        };
        var client = await GetClient();
        var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}parts");
        msg.Content = JsonContent.Create<Part>(part);
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
        var returnedJson = await response.Content.ReadAsStringAsync();

        var insertedPart = JsonSerializer.Deserialize<Part>(returnedJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        return insertedPart;
    }

    public static async Task Update(Part part)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;
        HttpRequestMessage msg = new(HttpMethod.Put, $"{Url}parts/{part.PartID}");
        msg.Content = JsonContent.Create<Part>(part);
        var client = await GetClient();
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }

    public static async Task Delete(string partID)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;
        HttpRequestMessage msg = new(HttpMethod.Delete, $"{Url}parts/{partID}");
        var client = await GetClient();
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }
}
