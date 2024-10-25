using PartsClient.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace PartsClient.Service
{
    public class PartsService : IPartsService
    {
        private readonly HttpClient client;

        public PartsService(HttpClient httpClient)
        {
            client = httpClient;
        }
        public async Task<IEnumerable<Part>> GetAll()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    return new List<Part>();

                string result = await client.GetStringAsync($"api/parts");

                return JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Part> Add(string partName, string supplier, string partType)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    return new Part();


                var part = new Part
                {
                    PartName = partName,
                    Suppliers = new List<string> { supplier },
                    PartID = string.Empty,
                    PartType = partType,
                    PartAvailableDate = DateTime.Now.Date
                };

                var response = await client.PostAsJsonAsync($"api/parts", part);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Part>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task Update(Part part)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    return;

                var response = await client.PutAsJsonAsync($"api/parts/{part.PartID}", part);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(string partID)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    return;

                var response = await client.DeleteAsync($"api/parts/{partID}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
