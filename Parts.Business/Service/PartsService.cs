using PartsClient.Dto.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace Parts.Business.Services
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
                string result = await client.GetStringAsync($"api/parts");

                return JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }) ?? new List<Part>();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately; you might want to log it or return an empty list.
                // Throwing the exception might not be ideal in a UI context.
                throw new ApplicationException("Failed to fetch parts", ex);
            }
        }

        public async Task<Part> Add(string partName, string supplier, string partType)
        {
            try
            {
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
                // Handle the exception appropriately.
                throw new ApplicationException("Failed to add part", ex);
            }
        }

        public async Task Update(Part part)
        {
            try
            {
                var response = await client.PutAsJsonAsync($"api/parts/{part.PartID}", part);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately.
                throw new ApplicationException("Failed to update part", ex);
            }
        }

        public async Task Delete(string partID)
        {
            try
            {
                var response = await client.DeleteAsync($"api/parts/{partID}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately.
                throw new ApplicationException("Failed to delete part", ex);
            }
        }
    }
}
