using Parts.Business.Services;
using PartsClient.Dto.Data;

namespace PartsClient.Service
{
    public class PartsClientService
    {
        private readonly IPartsService _partsService;

        public PartsClientService(IPartsService partsService)
        {
            _partsService = partsService;
        }

        public async Task<IEnumerable<Part>> GetAll()
        {
            if (!IsConnectedToInternet())
            {
                // Handle the case when there's no internet connectivity, e.g., return an empty list or cached data
                return new List<Part>();
            }

            return await _partsService.GetAll();
        }

        public async Task<Part> Add(string partName, string supplier, string partType)
        {
            if (!IsConnectedToInternet())
            {
                // Handle the case when there's no internet connectivity, e.g., return null or a default part
                return new Part();
            }

            return await _partsService.Add(partName, supplier, partType);
        }

        public async Task Update(Part part)
        {
            if (!IsConnectedToInternet())
            {
                // Handle the case when there's no internet connectivity, e.g., log or notify user
                return;
            }

            await _partsService.Update(part);
        }

        public async Task Delete(string partID)
        {
            if (!IsConnectedToInternet())
            {
                // Handle the case when there's no internet connectivity, e.g., log or notify user
                return;
            }

            await _partsService.Delete(partID);
        }

        private bool IsConnectedToInternet()
        {
            // Check the connectivity status using MAUI's Connectivity API
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
