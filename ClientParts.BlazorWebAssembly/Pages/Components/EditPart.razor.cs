using PartsClient.Dto.Data;
namespace ClientParts.BlazorWebAssembly.Pages.Components
{
    public partial class EditPart
    {
        private Part part = new Part();
        private string suppliersInput;

        private async Task HandleValidSubmit()
        {
            // Parse the suppliers input and populate the Suppliers list
            if (!string.IsNullOrWhiteSpace(suppliersInput))
            {
                part.Suppliers = suppliersInput.Split(',')
                                               .Select(s => s.Trim())
                                               .Where(s => !string.IsNullOrEmpty(s))
                                               .ToList();
            }

            // Here you can implement the logic to save the part data
            Console.WriteLine($"Part ID: {part.PartID}, Name: {part.PartName}, Suppliers: {part.SupplierString}, Type: {part.PartType}, Available Date: {part.PartAvailableDate}");

            // Redirect or perform other actions after successful submission
            // For example, using NavigationManager to redirect to another page
            // NavigationManager.NavigateTo("/parts");
        }
    }
}
